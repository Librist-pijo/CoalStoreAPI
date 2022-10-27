using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        Task CreateCategory(Categories category);
        Task UpdateCategory(Categories category);
        Task DeleteCategory(Categories category);
        Task GetCategories();
        Task GetCategoryByName(string categoryName);
    }
}
