// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Net.Http;

namespace BarbezDotEu.Provider.Interfaces
{
    public interface ICanFail
    {
        /// <summary>
        /// Gets or sets the <see cref="HttpRequestMessage"/> that indicates a fault that has occurred.
        /// </summary>
        public HttpResponseMessage FailedResponse { get; set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="HttpResponseMessage"/> indicates a fault.
        /// </summary>
        public bool HasFailed { get; }
    }
}
