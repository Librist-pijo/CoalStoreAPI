using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        Task Create(Categories category);
        Task Update(Categories category);
        Task Delete(int categoryId);
        Task<List<Categories>> Get();
        Task<Categories> GetByName(string categoryName);
        Task<Categories> GetById(int categoryId);
    }
}
