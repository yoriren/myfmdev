using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using myfm_dev_api.Interfaces;
using myfm_dev_api.Models;

namespace myfm_dev_api.Services
{
    public class ProductService : IProductService
    {
        private readonly IApiClient _apiClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ProductService> _logger;
        private const string CacheKey = "ProductsCache";

        public ProductService(
            IApiClient apiClient,
            IMemoryCache cache,
            ILogger<ProductService> logger)
        {
            _apiClient = apiClient;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProductsWithMarkup()
        {
            try
            {
                if (!_cache.TryGetValue(CacheKey, out IEnumerable<Product> products))
                {
                    var vendorProducts = await _apiClient.GetProductsAsync();

                    products = vendorProducts.Select(p => new Product
                    {
                        productId = p.productId,
                        name = p.name,
                        description = p.description,
                        unitPrice = p.unitPrice,
                        price = Math.Round(p.unitPrice * 1.2m, 2),
                        maximumQuantity = p.maximumQuantity
                    }).ToList();

                    // Cache for 5 minutes
                    _cache.Set(CacheKey, products, TimeSpan.FromMinutes(5));
                }

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products");
                throw;
            }
        }
    }
}