using API.ModelsDTO.CategoriesDTO;
using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface IProductsCategoriesService
    {
        public ResultData CreateProductsCategories(ProductsCategories productCategories);
        public ResultData UpdateProductsCategories(ProductsCategories productCategories);
        public ResultData DeleteProductsCategories(int Id);
        public List<ProductsCategories> GetProductsCategoriesByProductId(int productId);
    }
}
