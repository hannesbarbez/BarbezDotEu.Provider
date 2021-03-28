// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using Newtonsoft.Json;

namespace BarbezDotEu.Provider.DTO
{
    /// <summary>
    /// Implements a client authorization response DTO in accordance to the interface as defined and shared by Vimeo, Twitter, and others.
    /// </summary>
    public class PostClientAuthorizeResponse
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

        // Omitting implementation of "app" property as not (yet) relevant.
    }
}
