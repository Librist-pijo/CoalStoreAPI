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

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategories([FromBody] CreateCategoriesDTO categoriesDTO)
        {
            var value = await _categoriesService.CreateCategories(categoriesDTO);
            return Json(value);
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategories([FromBody] UpdateCategoriesDTO categoriesDTO)
        {
            var value = await _categoriesService.UpdateCategories(categoriesDTO);
            return Json(value);
        }
        [HttpDelete("delete-category/{categoryId}")]
        public async Task<IActionResult> DeleteCategories([FromBody] int categoryId)
        {
            var value = await _categoriesService.DeleteCategories(categoryId);
            return Json(value);
        }
        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var value = await _categoriesService.GetCategories();
            return Json(value);
        }
        [HttpGet("get-categories-by-name/{categoryName}")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            var value = await _categoriesService.GetCategoryByName(categoryName);
            return Json(value);
        }
    }
}
