using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.CategoriesDTO;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(IConfiguration config, ICategoriesService categoriesService)
        {
            _config = config;
            _categoriesService = categoriesService;
        }
        
        /// <summary>
        /// Creates new category
        /// </summary>
        /// <param name="categoriesDTO"></param>
        /// <returns></returns>
        [HttpPost("createcategory")]
        public async Task<IActionResult> CreateCategories([FromBody] CreateCategoriesDTO categoriesDTO)
        {
            var value = await _categoriesService.CreateCategories(categoriesDTO);
            return Json(value);
        }
       
        /// <summary>
        /// Updates category
        /// </summary>
        /// <param name="categoriesDTO"></param>
        /// <returns></returns>
        [HttpPut("updatecategory")]
        public async Task<IActionResult> UpdateCategories([FromBody] UpdateCategoriesDTO categoriesDTO)
        {
            var value = await _categoriesService.UpdateCategories(categoriesDTO);
            return Json(value);
        }
        
        /// <summary>
        /// Deletes category by id of category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete("deletecategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategories(int categoryId)
        {
            var value = await _categoriesService.DeleteCategories(categoryId);
            return Json(value);
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>List of all categories</returns>
        [HttpGet("getcategories")]
        public async Task<IActionResult> GetCategories()
        {
            var value = await _categoriesService.GetCategories();
            return Json(value);
        }
        
        /// <summary>
        /// Gets category by name of category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>One category that match category name</returns>
        [HttpGet("getcategoriesbyname/{categoryName}")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            var value = await _categoriesService.GetCategoryByName(categoryName);
            return Json(value);
        }
    }
}
