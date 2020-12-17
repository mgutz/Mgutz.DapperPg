using Mgutz.DapperPg.Dal;
using Mgutz.DapperPg.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mgutz.DapperPg.Services {

    public class ProductService : IProductService {
        private readonly IProductRepository _repository;
        private readonly RequestContext _context;
        private readonly ILogger _logger;

        /// <summary>This service is for products.</summary>
        /// <param name="ctx">Request context from HTTP headers or via gRPC</param>
        public ProductService(RequestContext ctx, ILogger<ProductService> logger, IProductRepository repo) {
            _repository = repo;
            _context = ctx;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAll() {
            // An example of how to access request context
            _logger.LogDebug("RequestContext.UserId={0}", _context.UserId);
            return await _repository.GetAll();
        }

        public async ValueTask<Product> GetById(int id) {
            return await _repository.GetById(id);
        }

        public async ValueTask<Product> Add(Product entity) {
            return await _repository.Add(entity);
        }

        public async ValueTask<Product> Update(Product entity, int id) {
            return await _repository.Update(entity, id);
        }

        public async Task Remove(int id) {
            await _repository.Remove(id);
        }
    }

}
