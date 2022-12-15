using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IProductsCategoriesRepository
    {
        Task Create(ProductsCategories productCategories);
        Task Update(ProductsCategories productCategories);
        Task Delete(int Id);
        Task<List<ProductsCategories>> Get();
        Task<ProductsCategories> GetById(int Id);
        Task<List<ProductsCategories>> GetByByCategoryId(int categoryId);
        Task<List<ProductsCategories>> GetByByProductId(int productId);
        Task<ProductsCategories> GetPair(int productId, int categoryId);
    }
}
