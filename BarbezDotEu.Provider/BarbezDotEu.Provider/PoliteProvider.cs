// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BarbezDotEu.Http;
using BarbezDotEu.Provider.Interfaces;
using Microsoft.Extensions.Logging;

namespace BarbezDotEu.Provider
{
    /// <summary>
    /// Implements an HTTP(S) client that supports rate limiting so that a polite integration
    /// with a third-party data provider can be implemented.
    /// </summary>
    public class PoliteProvider : IPoliteProvider
    {
        // Defines by how much to multiply the requiredSecondsbetweenCalls, if case calls are not done with regular intervals.
        private long multiplier = 1;
        private DateTime lastQueryTime;

        // 1 day = 86400" = (1 day * 24 * 60 * 60)
        private const double OneDayInSeconds = 86400;
        private const double OneMinuteInSeconds = 60;
        private const double OneHourInSeconds = 3600;

        /// <summary>
        /// Gets or sets the <see cref="ILogger"/>.
        /// </summary>
        protected ILogger Logger { get; }

        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Gets the number of seconds required to lapse before a next call to the data provider is considered polite.
        /// </summary>
        protected int RequiredSecondsInBetweenCalls { get; private set; }

        /// <summary>
        /// Constructs a new <see cref="PoliteProvider"/>.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger"/> to use for logging.</param>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> to use.</param>
        public PoliteProvider(ILogger logger, IHttpClientFactory httpClientFactory)
        {
            this.Logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc/>
        public bool IsPolite()
        {
            var delta = (DateTime.UtcNow - this.lastQueryTime).TotalSeconds;
            if (delta < RequiredSecondsInBetweenCalls * multiplier)
            {
                return false;
            }

            multiplier = 1;
            return true;
        }

        /// <inheritdoc/>
        public void SetMultiplier(long multiplier)
        {
            this.multiplier = multiplier;
        }

        /// <summary>
        /// Sends a <see cref="HttpRequestMessage"/> to the third-party provider, expecting a certain response.
        /// </summary>
        /// <typeparam name="T">The expected response content type.</typeparam>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to send to the third-party provider.</param>
        /// <param name="retryOnError"></param>
        /// <param name="waitingMinutesBeforeRetry">The number of minutes to wait before automatically retrying re-sending the request, if the intention is to retry again upon error.</param>
        /// <returns>The expected response content type, as well as other metadata, in case of an exception.</returns>
        protected virtual async Task<PoliteReponse<T>> Request<T>(HttpRequestMessage request, bool retryOnError = true, double waitingMinutesBeforeRetry = 15)
            where T : class
        {
            try
            {
                this.UpdateTimeOfLastCall(DateTime.UtcNow);
                var httpClient = this.httpClientFactory.CreateClient();
                using (var response = await httpClient.SendAsync(request))
                {
                    return await GetPoliteResponse<T>(response);
                }
            }
            catch (Exception exception)
            {
                if (retryOnError)
                {
                    Logger.LogWarning($"{nameof(PoliteProvider)} waiting {waitingMinutesBeforeRetry} minutes " +
                        $"because of an exception occuring during a {typeof(T)} request. No crash, going to try again. Exception details:{Environment.NewLine}{exception}.");

                    Thread.Sleep(TimeSpan.FromMinutes(waitingMinutesBeforeRetry));
                    var newRequest = await request.Clone();
                    return await Request<T>(newRequest, false);
                }

                throw;
            }
        }

        /// <summary>
        /// Converts a <see cref="HttpResponseMessage"/> into a <see cref="PoliteReponse{T}"/>.
        /// </summary>
        /// <typeparam name="T">The view model to deserialize a successful response into.</typeparam>
        /// <param name="response">The response to convert.</param>
        /// <returns>The <see cref="HttpResponseMessage"/> as <see cref="PoliteReponse{T}"/>.</returns>
        private async Task<PoliteReponse<T>> GetPoliteResponse<T>(HttpResponseMessage response) where T : class
        {
            var politeResponse = new PoliteReponse<T>(response);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    if (typeof(T) == typeof(string))
                    {

                    }
                    var content = await GetContent<T>(response, options);

                    // For Blazor, no "actual" DDD since needs async and constructors don't do async.
                    // https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0 
                    politeResponse.SetContent(content);
                }
                catch (JsonException exception)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    Logger.LogWarning($"{nameof(PoliteProvider)} expected JSON but got something else: " +
                        $"{contents}.{Environment.NewLine}" +
                        $"Exception details: {exception}.");
                    throw;
                }
            }
            else
            {
                Logger.LogInformation($"Unsuccessful response: HTTP code {response.StatusCode} - {response.ReasonPhrase}.");
            }

            return politeResponse;
        }

        private static async Task<T> GetContent<T>(HttpResponseMessage response, JsonSerializerOptions options) where T : class
        {
            if (typeof(T) == typeof(string))
            {
                var result = await response.Content.GetAsTextAsync();
                if (result is T content)
                    return content;
            }

            return await response.Content.ReadFromJsonAsync<T>(options);
        }

        /// <summary>
        /// Updates the date and time of when the last call to the provider, i.e. third-party resource, was made.
        /// </summary>
        /// <remarks>
        /// It is important to keep updating this number in order to continuously receive a correct answer from the <see cref="IsPolite"/> method.
        /// </remarks>
        /// <param name="lastQueryTime"></param>
        protected void UpdateTimeOfLastCall(DateTime lastQueryTime)
        {
            this.lastQueryTime = lastQueryTime;
        }

        /// <summary>
        /// Sets the number of calls per day as allowed to the provider, i.e. third-party resource.
        /// </summary>
        /// <remarks>
        /// The parameter string is expected to hold numeric values only.
        /// <see cref="SetRateLimitPerDay(long)"/>, <see cref="SetRateLimitPerHour(long)"/>, and <see cref="SetRateLimitPerMinute(long)"/> are mutually exclusive, hence the last called method determines the rate limiter.
        /// </remarks>
        /// <param name="callsPerDay">The max. number of allowed calls per day.</param>
        protected void SetRateLimitPerDay(long callsPerDay)
        {
            this.RequiredSecondsInBetweenCalls = (int)Math.Ceiling(OneDayInSeconds / callsPerDay);
        }

        /// <summary>
        /// Sets the number of calls per minute as allowed to the provider, i.e. third-party resource.
        /// </summary>
        /// <remarks>
        /// The parameter string is expected to hold numeric values only.
        /// <see cref="SetRateLimitPerDay(long)"/>, <see cref="SetRateLimitPerHour(long)"/>, and <see cref="SetRateLimitPerMinute(long)"/> are mutually exclusive, hence the last called method determines the rate limiter.
        /// </remarks>
        /// <param name="callsPerMinute">The max. number of allowed calls per minute.</param>
        protected void SetRateLimitPerMinute(long callsPerMinute)
        {
            this.RequiredSecondsInBetweenCalls = (int)Math.Ceiling(OneMinuteInSeconds / callsPerMinute);
        }

        /// <summary>
        /// Sets the number of calls per hour as allowed to the provider, i.e. third-party resource.
        /// </summary>
        /// <remarks>
        /// The parameter string is expected to hold numeric values only.
        /// <see cref="SetRateLimitPerDay(long)"/>, <see cref="SetRateLimitPerHour(long)"/>, and <see cref="SetRateLimitPerMinute(long)"/> are mutually exclusive, hence the last called method determines the rate limiter.
        /// </remarks>
        /// <param name="callsPerHour">The max. number of allowed calls per hour.</param>
        protected void SetRateLimitPerHour(long callsPerHour)
        {
            this.RequiredSecondsInBetweenCalls = (int)Math.Ceiling(OneHourInSeconds / callsPerHour);
        }
    }
}
