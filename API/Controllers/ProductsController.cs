using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.ProductsDTO;
using API.Repositories.Models;
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

        [HttpPost("create-product-categories-mapping")]
        public IActionResult CreateProductsCategoryMapping([FromBody] ProductsCategories productsCategories)
        {
            var value = _ProductsService.CreateProductsCategoriesMapping(productsCategories);
            return Json(value);
        }

        [HttpPut("update-product")]
        public IActionResult UpdateProducts([FromBody] UpdateProductsDTO ProductsDTO)
        {
            var value = _ProductsService.UpdateProducts(ProductsDTO);
            return Json(value);
        }

        [HttpPut("update-product-categories-mapping")]
        public IActionResult UpdateCreateProductsCategoryMapping([FromBody] ProductsCategories productsCategories)
        {
            var value = _ProductsService.UpdateProductsCategoriesMapping(productsCategories);
            return Json(value);
        }

        [HttpDelete("delete-product")]
        public IActionResult DeleteProducts([FromBody] int productId)
        {
            var value = _ProductsService.DeleteProducts(productId);
            return Json(value);
        }

        [HttpDelete("delete-product-categories-mapping")]
        public IActionResult DeleteCreateProductsCategoryMapping(int Id)
        {
            var value = _ProductsService.DeleteProductsCategoriesMapping(Id);
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

        [HttpGet("get-products-with-category")]
        public IActionResult GetProductsWithCategory()
        {
            var value = _ProductsService.GetProductsWithCategory();
            return Json(value);
        }
    }
}
