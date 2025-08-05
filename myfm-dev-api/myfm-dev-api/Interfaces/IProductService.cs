using myfm_dev_api.Models;

namespace myfm_dev_api.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsWithMarkup();
    }
}
