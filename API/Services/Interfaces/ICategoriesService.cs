using API.ModelsDTO;
using API.ModelsDTO.CategoriesDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<ResultData> CreateCategories(CreateCategoriesDTO categoriesDTO);
        public ResultData UpdateCategories(UpdateCategoriesDTO categoriesDTO);
        public ResultData DeleteCategories(int categoryId);
        public Categories GetCategoryByName(string categoryName);
        public List<Categories> GetCategories();
    }
}
