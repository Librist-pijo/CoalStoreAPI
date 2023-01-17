using API.ModelsDTO.CategoriesDTO;
using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface IProductsCategoriesService
    {
        public Task<ResultData> CreateProductsCategories(ProductsCategories productCategories);
        public Task<ResultData> UpdateProductsCategories(ProductsCategories productCategories);
        public Task<ResultData> DeleteProductsCategories(int Id);
        public Task<List<ProductsCategories>> GetProductsCategoriesByProductId(int productId);
    }
}
