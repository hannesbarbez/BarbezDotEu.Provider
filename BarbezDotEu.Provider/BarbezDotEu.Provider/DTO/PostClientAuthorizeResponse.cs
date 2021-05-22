// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Text.Json.Serialization;

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
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the authorization scope.
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        // Omitting implementation of "app" property.
    }
}
