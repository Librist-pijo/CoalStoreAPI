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
        public IActionResult CreateCategories([FromBody] CreateCategoriesDTO categoriesDTO)
        {
            var value = _categoriesService.CreateCategories(categoriesDTO);
            return Json(value);
        }
        [HttpPut("update-category")]
        public IActionResult UpdateCategories([FromBody] UpdateCategoriesDTO categoriesDTO)
        {
            var value = _categoriesService.UpdateCategories(categoriesDTO);
            return Json(value);
        }
        [HttpDelete("delete-category")]
        public IActionResult DeleteCategories([FromBody] int categoryId)
        {
            var value = _categoriesService.DeleteCategories(categoryId);
            return Json(value);
        }
        [HttpGet("get-categories")]
        public IActionResult GetCategories()
        {
            var value = _categoriesService.GetCategories();
            return Json(value);
        }
        [HttpGet("get-categories-by-name")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            var value = _categoriesService.GetCategoryByName(categoryName);
            return Json(value);
        }
    }
}
