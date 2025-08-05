using myfm_dev_api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myfm_dev_api.Interfaces
{
    public interface IApiClient
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}