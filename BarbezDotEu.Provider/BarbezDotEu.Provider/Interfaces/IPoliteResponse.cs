// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Net.Http;

namespace BarbezDotEu.Provider.Interfaces
{
    /// <summary>
    /// Defines a <see cref="IPoliteResponse"/> to a <see cref="HttpRequestMessage"/> requested by an <see cref="IPoliteProvider"/>.
    /// </summary>
    public interface IPoliteResponse<T> where T : class
    {
        /// <summary>
        /// Gets the actual content of a successful response to a <see cref="HttpRequestMessage"/>.
        /// </summary>
        public T Content { get; }

        /// <summary>
        /// Gets the <see cref="HttpRequestMessage"/> that indicates a fault that has occurred.
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="System.Net.Http.HttpResponseMessage"/> indicates a fault.
        /// </summary>
        public bool HasFailed { get; }

        /// <summary>
        /// Sets the the actual response content.
        /// </summary>
        /// <param name="content">The response content to set.</param>
        void SetContent(T content);
    }
}
