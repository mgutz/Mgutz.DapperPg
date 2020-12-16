using Mgutz.DapperPg.Models;
using Mgutz.DapperPg.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mgutz.DapperPg.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAll() {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetById(int id) {
            var product = await _productRepository.GetById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product entity) {
            return Ok(await _productRepository.Add(entity));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(Product entity, int id) {
            return Ok(await _productRepository.Update(entity, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            await _productRepository.Remove(id);
            return Ok();
        }
    }
}
