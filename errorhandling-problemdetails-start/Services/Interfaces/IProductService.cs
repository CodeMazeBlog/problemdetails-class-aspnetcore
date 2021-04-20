using ErrorHandlingProblemDetails.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErrorHandlingProblemDetails.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllPrpoducts();
        Task<Product> GetProductById(int id);
        Task CreateNewProduct(Product product);
        Task<Product> GetProductByName(string name);
    }
}
