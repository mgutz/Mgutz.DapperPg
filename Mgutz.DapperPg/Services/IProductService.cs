using Mgutz.DapperPg.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mgutz.DapperPg.Services {

    public interface IProductService {
        Task<IEnumerable<Product>> GetAll();
        ValueTask<Product> GetById(int id);
        ValueTask<Product> Add(Product entity);
        ValueTask<Product> Update(Product entity, int id);
        Task Remove(int id);
    }

}
