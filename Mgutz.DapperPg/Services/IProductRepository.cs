using Mgutz.DapperPg.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mgutz.DapperPg.Services {
    public interface IProductRepository {
        ValueTask<Product> GetById(int id);
        ValueTask<Product> AddProduct(Product entity);
        ValueTask<Product> UpdateProduct(Product entity, int id);
        Task RemoveProduct(int id);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
