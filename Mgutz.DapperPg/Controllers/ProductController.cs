using Mgutz.DapperPg.Models;
using Mgutz.DapperPg.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mgutz.DapperPg.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductService _productService;

        public ProductController(IProductService productRepository) {
            _productService = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAll() {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetById(int id) {
            var product = await _productService.GetById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product entity) {
            return Ok(await _productService.Add(entity));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(Product entity, int id) {
            return Ok(await _productService.Update(entity, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            await _productService.Remove(id);
            return Ok();
        }
    }
}
