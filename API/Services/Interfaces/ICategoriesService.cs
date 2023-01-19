using API.ModelsDTO;
using API.ModelsDTO.CategoriesDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<ResultData> CreateCategories(CreateCategoriesDTO categoriesDTO);
        public Task<ResultData> UpdateCategories(UpdateCategoriesDTO categoriesDTO);
        public Task<ResultData> DeleteCategories(int categoryId);
        public Task<Categories> GetCategoryByName(string categoryName);
        public Task<List<Categories>> GetCategories();
    }
}
