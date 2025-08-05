using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using myfm_dev_api.Interfaces;
using myfm_dev_api.Models;

namespace myfm_dev_api.Clients
{
    public class MyfmApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly SemaphoreSlim _rateLimiter = new SemaphoreSlim(5, 5);

        public MyfmApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri("https://myfm-dev.cityfm.com.au/xapi/api/");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            await _rateLimiter.WaitAsync();
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "product/get-product-list");

                request.Headers.Add("X-Api-Key", _configuration["VendorApiKey"]);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }
            finally
            {
                // Release after 60 seconds
                _ = Task.Delay(TimeSpan.FromSeconds(60)).ContinueWith(_ => _rateLimiter.Release());
            }
        }
    }
}
