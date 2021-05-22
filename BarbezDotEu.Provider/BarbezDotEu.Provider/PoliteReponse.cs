// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Net.Http;
using BarbezDotEu.Provider.Interfaces;

namespace BarbezDotEu.Provider
{
    /// <inheritdoc/>
    public class PoliteReponse<T> : IPoliteResponse<T> where T : class
    {
        /// <summary>
        /// Constructs a new <see cref="PoliteReponse{T}"/> from a given <see cref="HttpResponseMessage"/>.
        /// </summary>
        /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/> to construct this <see cref="IPoliteResponse{T}"/> from.</param>
        public PoliteReponse(HttpResponseMessage httpResponseMessage)
        {
            this.HttpResponseMessage = httpResponseMessage;
        }

        /// <inheritdoc/>
        public T Content { get; private set; }

        /// <inheritdoc/>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <inheritdoc/>
        public bool HasFailed => !HttpResponseMessage.IsSuccessStatusCode;

        /// <inheritdoc/>
        public void SetContent(T content)
        {
            this.Content = content;
        }
    }
}
