using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.Products;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IProductsService _ProductsService;

        public ProductsController(IConfiguration config, IProductsService ProductsService)
        {
            _config = config;
            _ProductsService = ProductsService;
        }

        [HttpPost("create-product")]
        public IActionResult CreateProducts([FromBody] CreateProductsDTO ProductsDTO)
        {
            var value = _ProductsService.CreateProducts(ProductsDTO);
            return Json(value);
        }
        [HttpPut("update-product")]
        public IActionResult UpdateProducts([FromBody] UpdateProductsDTO ProductsDTO)
        {
            var value = _ProductsService.UpdateProducts(ProductsDTO);
            return Json(value);
        }
        [HttpDelete("delete-product")]
        public IActionResult DeleteProducts([FromBody] DeleteProductsDTO ProductsDTO)
        {
            var value = _ProductsService.DeleteProducts(ProductsDTO);
            return Json(value);
        }
        [HttpGet("get-products")]
        public IActionResult GetProducts()
        {
            var value = _ProductsService.GetProducts();
            return Json(value);
        }
        [HttpGet("get-product-by-name")]
        public IActionResult GetProductByName(string productName)
        {
            var value = _ProductsService.GetProductByName(productName);
            return Json(value);
        }
    }
}
