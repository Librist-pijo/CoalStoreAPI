using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        Task CreateCategory(Categories category);
        Task UpdateCategory(Categories category);
        Task DeleteCategory(Categories category);
        Task<List<Categories>> GetCategories();
        Task<Categories> GetCategoryByName(string categoryName);
        Task<Categories> GetCategoryById(int categoryId);
    }
}
