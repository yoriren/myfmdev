using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using myfm_dev_api.Interfaces;
using myfm_dev_api.Models;
using myfm_dev_api.Services;
using Xunit;

namespace myfm_dev_api.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IApiClient> _mockApiClient;
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly ILogger<ProductService> _logger;

        public ProductServiceTests()
        {
            _mockApiClient = new Mock<IApiClient>();
            _mockCache = new Mock<IMemoryCache>();
            _logger = new Mock<ILogger<ProductService>>().Object;
        }

        [Fact]
        public async Task GetProductsWithMarkup_ReturnsProductsWith20PercentMarkup()
        {
            // Arrange
            var testProducts = new List<Product>
            {
                new Product { 
                    productId = "2", 
                    name = "Cloud ABC",
                    description = "For testing purpose.",
                    unitPrice = 100m, 
                    price = 120m,
                    maximumQuantity = 100
                }
            };

            _mockApiClient.Setup(x => x.GetProductsAsync())
                .ReturnsAsync(testProducts);

            object dummy;
            _mockCache.Setup(x => x.TryGetValue(It.IsAny<string>(), out dummy))
                .Returns(false);

            _mockCache.Setup(x => x.CreateEntry(It.IsAny<string>()))
                .Returns(Mock.Of<ICacheEntry>()); 

            var service = new ProductService(
                _mockApiClient.Object,
                _mockCache.Object,
                _logger);

            // Act
            var result = await service.GetProductsWithMarkup();

            // Assert
            Assert.Single(result);
            Assert.Equal(120m, result.First().price);
            Assert.Equal(100m, result.First().unitPrice);
            Assert.Equal("2", result.First().productId);
        }
    }
}