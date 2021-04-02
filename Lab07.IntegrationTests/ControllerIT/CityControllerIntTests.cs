using Lab06.MVC;
using Lab07.IntegrationTests.FactoryConfig;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Lab07.IntegrationTests.ControllerIT
{
    public class CityControllerIntTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CityControllerIntTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                {
                    services.AddTransient<IPolicyEvaluator, FakePolicyEvaluator>();
                })).CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });
        }

        [Fact]
        public async Task AddCity_Get_ReturnView()
        {
            var response = await _client.GetAsync("/City/AddCity");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html; charset=utf-8",
                         response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task AddCity_Post_EmptydModel_ReturnViewWithErrorMessage()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/City/AddCity");

            var cityModel = new Dictionary<string, string>
            {
                { "Name", string.Empty }
            };

            postRequest.Content = new FormUrlEncodedContent(cityModel);
            var response = await _client.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Empty name", responseString);
        }

        [Fact]
        public async Task AddCity_Post_InputDbExistingCity_ReturnViewWithErrorMessage()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/City/AddCity");

            var cityModel = new Dictionary<string, string>
            {
                { "Name", "Minsk" } // Minsk already exists in the Db
            };

            postRequest.Content = new FormUrlEncodedContent(cityModel);
            var response = await _client.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Contains("The city already exist", responseString);
        }

        [Fact]
        public async Task AddCity_Post_ValidModel_RedirectToHomeIndex()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/City/AddCity");
            var cityModel = new Dictionary<string, string>
            {
                { "Name", "Moscow" }
            };

            postRequest.Content = new FormUrlEncodedContent(cityModel);
            var response = await _client.SendAsync(postRequest);
            var location = response.Headers.Location.OriginalString;

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode); // statuscode = 302
            Assert.Equal("/Home/Index", location);
        }
    }
}