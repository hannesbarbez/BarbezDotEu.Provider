// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace BarbezDotEu.Provider.DTO
{
    /// <summary>
    /// Mocks headers that would've been sent typically by Microsoft Edge during the first half of 2021.
    /// </summary>
    /// <remarks>
    /// The purpose of this class is to be used by a provider when said provider does not want to look too much like a bot.
    /// How can a bot not look like a bot? By, for example, looking like Edge instead.
    /// </remarks>
    public class EdgeMockingRequestHeaderCollection
    {
        /// <summary>
        /// Gets Edge style accept headers.
        /// </summary>
        public MediaTypeWithQualityHeaderValue[] AcceptHeaders { get; }

        /// <summary>
        /// Gets an Edge style user agent header.
        /// </summary>
        public string UserAgent { get; }

        /// <summary>
        /// Gets an Edge style accept header.
        /// </summary>
        public StringWithQualityHeaderValue AcceptLanguage { get; }

        /// <summary>
        /// Gets an Edge style referrer header.
        /// </summary>
        public Uri Referrer { get; }

        /// <summary>
        /// Gets an Edge style cache-control header.
        /// </summary>
        public CacheControlHeaderValue CacheControl { get; }

        /// <summary>
        /// Prepares a given <see cref="HttpRequestMessage"/> with headers sent typically by Microsoft Edge during the first half of 2021.
        /// </summary>
        /// <param name="httpRequestMessage">The <see cref="HttpRequestMessage"/> to adjust.</param>
        public HttpRequestMessage Prep(HttpRequestMessage httpRequestMessage)
        {
            foreach (var acceptHeader in this.AcceptHeaders)
                httpRequestMessage.Headers.Accept.Add(acceptHeader);

            httpRequestMessage.Headers.AcceptLanguage.Add(this.AcceptLanguage);
            httpRequestMessage.Headers.Referrer = this.Referrer;
            httpRequestMessage.Headers.CacheControl = this.CacheControl;
            httpRequestMessage.Headers.Pragma.Add(this.Pragma);
            httpRequestMessage.Headers.Connection.Add(this.Connection);
            httpRequestMessage.Headers.Add("User-Agent", this.UserAgent);

            foreach (var header in this.Others)
                httpRequestMessage.Headers.Add(header.Key, header.Value);

            return httpRequestMessage;
        }

        /// <summary>
        /// Gets an Edge style pragma header.
        /// </summary>
        public NameValueHeaderValue Pragma { get; }

        /// <summary>
        /// Gets a collection of non-standard headers, including:
        /// - Edge style do-not-track header;
        /// - Edge style sec-fetch-site header;
        /// - Edge style sec-fetch-mode header;
        /// - Edge style sec-fetch-dest header..
        /// </summary>
        public KeyValuePair<string, string>[] Others { get; }

        /// <summary>
        /// Gets an Edge style connection header.
        /// </summary>
        public string Connection { get; }

        /// <summary>
        /// Constructs a new <see cref="EdgeMockingRequestHeaderCollection"/>.
        /// </summary>
        /// <param name="referrer">The referrer to set.</param>
        public EdgeMockingRequestHeaderCollection(string referrer)
        {
            var acceptHeaderJson = new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json);
            var acceptHeaderText = new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Plain);
            var acceptHeaderAnything = new MediaTypeWithQualityHeaderValue("*/*");
            this.AcceptHeaders = new[] { acceptHeaderJson, acceptHeaderText, acceptHeaderAnything };
            this.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36 Edg/89.0.774.63";
            this.AcceptLanguage = new StringWithQualityHeaderValue("en-US", 0.9);
            this.Referrer = new Uri(referrer);
            this.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            this.Pragma = new NameValueHeaderValue("pragma", "no-cache");
            this.Connection = "keep-alive";

            this.Others = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("Sec-Fetch-Site","same-site"),
                new KeyValuePair<string, string>("Sec-Fetch-Mode","cors"),
                new KeyValuePair<string, string>("Sec-Fetch-Dest","empty"),
                new KeyValuePair<string, string>("DNT","1"),
            };
        }
    }
}
