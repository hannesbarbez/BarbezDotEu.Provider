// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Net.Http;
using BarbezDotEu.Provider.Interfaces;

namespace BarbezDotEu.Provider
{
    /// <inheritdoc/>
    /// <summary>
    /// Constructs a new <see cref="PoliteReponse{T}"/> from a given <see cref="HttpResponseMessage"/>.
    /// </summary>
    /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/> to construct this <see cref="IPoliteResponse{T}"/> from.</param>
    public class PoliteReponse<T>(HttpResponseMessage httpResponseMessage) : IPoliteResponse<T> where T : class
    {

        /// <inheritdoc/>
        public T Content { get; private set; }

        /// <inheritdoc/>
        public HttpResponseMessage HttpResponseMessage { get; } = httpResponseMessage;

        /// <inheritdoc/>
        public bool HasFailed => !HttpResponseMessage.IsSuccessStatusCode;

        /// <inheritdoc/>
        public void SetContent(T content)
        {
            this.Content = content;
        }
    }
}
