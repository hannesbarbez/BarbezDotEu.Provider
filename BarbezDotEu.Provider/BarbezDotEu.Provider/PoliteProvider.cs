// Copyright (c) Hannes Barbez. All rights reserved.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BarbezDotEu.Http;
using BarbezDotEu.Provider.Interfaces;
using Marvin.StreamExtensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarbezDotEu.Provider
{
    /// <summary>
    /// Implements a provider with support for rate limiting as imposed by the third-party data provider.
    /// </summary>
    public class PoliteProvider : IPoliteProvider
    {
        // Defines by how much to multiply the requiredSecondsbetweenCalls, if case calls are not done with regular intervals.
        private long multiplier = 1;
        private DateTime lastQueryTime;
        private int requiredSecondsBetweenCalls;
        private readonly ILogger<IHostedService> logger;
        private readonly HttpClient httpClient;

        public PoliteProvider(ILogger<IHostedService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClient = httpClientFactory.CreateClient();
        }

        /// <inheritdoc/>
        public bool IsPolite()
        {
            var delta = (DateTime.UtcNow - this.lastQueryTime).TotalSeconds;
            if (delta < requiredSecondsBetweenCalls * multiplier)
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

        /// <inheritdoc/>
        protected async Task<T> Request<T>(HttpRequestMessage request, bool retryOnError = true, double waitingMinutesBeforeRetry = 15)
            where T : IHasHttpResponseMessage
        {
            try
            {
                this.UpdateTimeOfLastCall(DateTime.UtcNow);
                using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                if (!response.IsSuccessStatusCode)
                    return default;

                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var result = await stream.ReadAndDeserializeFromJsonAsync<T>();
                return result;
            }
            catch (Exception exception)
            {
                if (retryOnError)
                {
                    logger.LogWarning($"{nameof(PoliteProvider)} waiting {waitingMinutesBeforeRetry} minutes " +
                        $"because of an exception occuring during a {typeof(T)} request. No crash, going to try again. Exception details below.{Environment.NewLine}{exception}.");

                    Thread.Sleep(TimeSpan.FromMinutes(waitingMinutesBeforeRetry));
                    var newRequest = await HttpRequestMessageCloner.Clone(request);
                    return await Request<T>(newRequest, false);
                }

                throw;
            }
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
        /// <see cref="SetRateLimitPerDay(string)"/> and <see cref="SetRateLimitPerMinute(string)"/> are mutually exclusive, and the one last calls determines the rate limiter.
        /// </remarks>
        /// <param name="callsPerDayString">The max. number of allowed calls per day.</param>
        protected void SetRateLimitPerDay(string callsPerDayString)
        {
            // 1 day = 500 calls => 86400" = (1 day * 24 * 60 * 60)
            double dayInSeconds = 86400;
            var callsPerDay = double.Parse(callsPerDayString);
            this.requiredSecondsBetweenCalls = (int)Math.Ceiling(dayInSeconds / callsPerDay);
        }

        /// <summary>
        /// Sets the number of calls per minute as allowed to the provider, i.e. third-party resource.
        /// </summary>
        /// <remarks>
        /// The parameter string is expected to hold numeric values only.
        /// <see cref="SetRateLimitPerDay(string)"/> and <see cref="SetRateLimitPerMinute(string)"/> are mutually exclusive, and the one last calls determines the rate limiter.
        /// </remarks>
        /// <param name="callsPerDayString">The max. number of allowed calls per minute.</param>
        protected void SetRateLimitPerMinute(string callsPerMinuteString)
        {
            var minuteInSeconds = 60;
            var callsPerMinute = double.Parse(callsPerMinuteString);
            this.requiredSecondsBetweenCalls = (int)Math.Ceiling(minuteInSeconds / callsPerMinute);
        }
    }
}
