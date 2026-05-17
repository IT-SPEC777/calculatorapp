using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CalculatorApp.Tests.IntegrationTests
{
    public class CalculatorApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CalculatorApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Add_ReturnsCorrectResult()
        {
            var response = await _client.GetAsync("/calculator/add?a=5&b=3");
            var result = await response.Content.ReadAsStringAsync();
            
            response.EnsureSuccessStatusCode();
            Assert.Equal("8", result);
        }
    }
}