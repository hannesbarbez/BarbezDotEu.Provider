// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarbezDotEu.Provider.Tests
{
    public class PoliteProviderCan
    {
        private readonly IHost _host;

        public PoliteProviderCan()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging();
                    services.AddHttpClient();
                }).Build();
        }

        [Fact]
        public async Task GetInsiderFinancialRssXmlAsync()
        {
            // Arrange
            var httpClientFactory = _host.Services.GetService<IHttpClientFactory>();
            var logger = _host.Services.GetService<ILogger<TestablePoliteProvider>>();
            Assert.NotNull(httpClientFactory);
            Assert.NotNull(logger);

            // Act
            var provider = new TestablePoliteProvider(logger, httpClientFactory);
            (string html, string uri) = await provider.GetInsiderFinancialRssXml();

            // Assert
            Assert.NotNull(html);
            Assert.NotNull(uri);
        }
    }
}
