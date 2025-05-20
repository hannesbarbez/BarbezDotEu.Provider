using BarbezDotEu.Http;
using Microsoft.Extensions.Logging;

namespace BarbezDotEu.Provider.Tests
{
    public class TestablePoliteProvider(ILogger logger, IHttpClientFactory httpClientFactory) : PoliteProvider(logger, httpClientFactory)
    {
        public async Task<(string html, string uri)> GetInsiderFinancialRssXml()
        {
            string uri = "https://www.insiderfinancial.com/post/rss.xml";
            return (await Get(uri), uri);
        }

        private async Task<string> Get(string url)
        {
            var uri = new Uri(url);
            var headers = new EdgeMockingRequestHeaderCollection($"https://{uri.Host}/");
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request = headers.Prep(request);
            var result = await this.Request<string>(request);
            if (result != null && result.HasFailed)
            {
                this.Logger.LogError("Request to {uri} failed: {error}", url, result.HttpResponseMessage);
                return null;
            }

            return result.Content;
        }
    }
}
