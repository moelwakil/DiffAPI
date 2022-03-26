using DiffAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DiffAPI.IntegrationTests
{
    public class DiffAPIIntegrationTests
    {   
        string _baseUrl = "/v1/diff";
        TestServer _server;
        HttpClient _client;

        public DiffAPIIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task PutLeftAsync()
        {
            var url = $"{_baseUrl}/1/left";
            var response = await _client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(new DiffContent { Data = new byte[] { 0, 1, 1, 0 } }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PutRightAsync()
        {
            var url = $"{_baseUrl}/1/right";
            var response = await _client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(new DiffContent { Data = new byte[] { 0, 1, 1, 0 } }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetDiffDataAsync()
        {
            var leftUrl = $"{_baseUrl}/1/left";
            var rightUrl = $"{_baseUrl}/1/right";
            var url = $"{_baseUrl}/1";
            await _client.PutAsync(leftUrl, new StringContent(JsonConvert.SerializeObject(new DiffContent { Data = new byte[] { 0, 1, 1, 0 } }), Encoding.UTF8, "application/json"));
            await _client.PutAsync(rightUrl, new StringContent(JsonConvert.SerializeObject(new DiffContent { Data = new byte[] { 0, 1, 1, 0 } }), Encoding.UTF8, "application/json"));
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetDiffDataWithNullDataAsync()
        {
            var url = $"{_baseUrl}/1";
            var response = await _client.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
