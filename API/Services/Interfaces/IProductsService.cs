using API.ModelsDTO;
using API.Repositories.Models;
using API.ModelsDTO.ProductsDTO;

namespace API.Services.Interfaces
{
    public interface IProductsService
    {
        public Task<ResultData> CreateProducts(CreateProductsDTO ProductsDTO);
        public Task<ResultData> UpdateProducts(UpdateProductsDTO ProductsDTO);
        public Task<ResultData> DeleteProducts(int productsId);
        public Task<Products> GetProductByName(string productName);
        public Task<Products> GetProductById(int productId);
        public Task<List<ProductsWithCategory>> GetProductsWithCategory();
        public Task<List<Products>> GetProducts();
        public Task<ResultData> CreateProductsCategoriesMapping(ProductsCategories productsCategories);
        public Task<ResultData> UpdateProductsCategoriesMapping(ProductsCategories productsCategories);
        public Task<ResultData> DeleteProductsCategoriesMapping(int Id);
    }

    public class ProductsWithCategory
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Id { get; set; }
    }
}
