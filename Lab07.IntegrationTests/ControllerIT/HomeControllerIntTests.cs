using Lab06.MVC;
using Lab07.IntegrationTests.FactoryConfig;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Lab07.IntegrationTests.ControllerIT
{
    public class HomeControllerIntTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HomeControllerIntTests(TestingWebAppFactory<Startup> factory)
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
        public async Task Index_Get_ReturnView()
        {
            var response = await _client.GetAsync("/Home/Index");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html; charset=utf-8",
                         response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Index_Post_InvalidCityIdArgument_ReturnBadRequest()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/index");

            var arguments = new Dictionary<string, string>
            {
                { "CityId", "0" },
                { "StartDate", DateTime.Now.ToString() }
            };

            postRequest.Content = new FormUrlEncodedContent(arguments);
            var response = await _client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Index_Post_PassValidArguments_ReturnViewIndex()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/index");

            var arguments = new Dictionary<string, string>
            {
                { "CityId", "1" }, // cityId is valid (exist in the db)
                { "StartDate", DateTime.Now.ToString() }
            };

            postRequest.Content = new FormUrlEncodedContent(arguments);
            var response = await _client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
