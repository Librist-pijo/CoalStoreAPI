using API.ModelsDTO;
using API.ModelsDTO.CategoriesDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface ICategoriesService
    {
        public ResultData CreateCategories(CreateCategoriesDTO categoriesDTO);
        public ResultData UpdateCategories(UpdateCategoriesDTO categoriesDTO);
        public ResultData DeleteCategories(DeleteCategoriesDTO deleteCategoriesDTO);
        public Categories GetCategoryByName(string categoryName);
        public List<Categories> GetCategories();
    }
}
