// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Net.Http;

namespace BarbezDotEu.Provider.Interfaces
{
    public interface IHasHttpResponseMessage
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
