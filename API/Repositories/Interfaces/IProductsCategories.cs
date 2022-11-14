using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IProductsCategories
    {
        Task Create(ProductsCategories productCategories);
        Task Update(ProductsCategories productCategories);
        Task Delete(ProductsCategories productCategories);
        Task<List<ProductsCategories>> Get();
        Task<List<ProductsCategories>> GetByByCategoryId(int categoryId);
        Task<List<ProductsCategories>> GetByByProductId(int productId);
    }
}
