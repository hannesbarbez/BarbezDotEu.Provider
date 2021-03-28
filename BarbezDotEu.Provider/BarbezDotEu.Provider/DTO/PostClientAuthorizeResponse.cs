// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Net.Http;
using BarbezDotEu.Provider.Interfaces;
using Newtonsoft.Json;

namespace BarbezDotEu.Provider.DTO
{
    /// <summary>
    /// Implements a client authorization response DTO in accordance to the interface as defined and shared by Vimeo, Twitter, and others.
    /// </summary>
    public class PostClientAuthorizeResponse : ICanFail
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the authorization scope.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <inheritdoc/>
        public HttpResponseMessage FailedResponse { get; set; }

        /// <inheritdoc/>
        public bool HasFailed => FailedResponse != null;

        // Omitting implementation of "app" property as not (yet) relevant.
    }
}
