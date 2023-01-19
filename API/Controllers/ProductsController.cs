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

        /// <summary>
        /// Creates new product
        /// </summary>
        /// <param name="ProductsDTO"></param>
        /// <returns></returns>
        [HttpPost("createproduct")]
        public async Task<IActionResult> CreateProducts([FromBody] CreateProductsDTO ProductsDTO)
        {
            var value = await _ProductsService.CreateProducts(ProductsDTO);
            return Json(value);
        }

        /// <summary>
        /// Creates mapping between category and product
        /// </summary>
        /// <param name="productsCategories"></param>
        /// <returns></returns>
        [HttpPost("createproductcategoriesmapping")]
        public async Task<IActionResult> CreateProductsCategoryMapping([FromBody] ProductsCategories productsCategories)
        {
            var value = await _ProductsService.CreateProductsCategoriesMapping(productsCategories);
            return Json(value);
        }

        /// <summary>
        /// Updates products
        /// </summary>
        /// <param name="ProductsDTO"></param>
        /// <returns></returns>
        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProducts([FromBody] UpdateProductsDTO ProductsDTO)
        {
            var value = await _ProductsService.UpdateProducts(ProductsDTO);
            return Json(value);
        }

        /// <summary>
        /// Updates mapping between products and categories
        /// </summary>
        /// <param name="productsCategories"></param>
        /// <returns></returns>
        [HttpPut("updateproductcategoriesmapping")]
        public async Task<IActionResult> UpdateCreateProductsCategoryMapping([FromBody] ProductsCategories productsCategories)
        {
            var value = await _ProductsService.UpdateProductsCategoriesMapping(productsCategories);
            return Json(value);
        }

        /// <summary>
        /// Deletes product by id of product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("deleteproduct/{productId}")]
        public async Task<IActionResult> DeleteProducts([FromBody] int productId)
        {
            var value = await _ProductsService.DeleteProducts(productId);
            return Json(value);
        }

        /// <summary>
        /// Delete productCategory mapping by id of ProductCategories
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("deleteproductcategoriesmapping/{Id}")]
        public async Task<IActionResult> DeleteCreateProductsCategoryMapping(int Id)
        {
            var value = await _ProductsService.DeleteProductsCategoriesMapping(Id);
            return Json(value);
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of all products</returns>
        [HttpGet("getproducts")]
        public async Task<IActionResult> GetProducts()
        {
            var value = await _ProductsService.GetProducts();
            return Json(value);
        }

        /// <summary>
        /// Gets product by name of product
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>One product that match product name</returns>
        [HttpGet("getproductbyname/{productName}")]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            var value = await _ProductsService.GetProductByName(productName);
            return Json(value);
        }

        /// <summary>
        /// Gets all products with categories
        /// </summary>
        /// <returns>List of all products with their categories</returns>
        [HttpGet("getproductswithcategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var value = await _ProductsService.GetProductsWithCategory();
            return Json(value);
        }
    }
}
