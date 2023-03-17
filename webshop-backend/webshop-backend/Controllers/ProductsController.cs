using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webshop_backend.Models;
using webshop_backend.Services;

namespace webshop_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto)
        {

            var productId = await _productService.CreateProduct(dto);


            return Ok(new { id = productId });
        }

        [HttpGet("all")]
        public async Task<List<Product>> AllProducts()
        {
            return await _productService.GetAllProducts();
        }
    }

}
