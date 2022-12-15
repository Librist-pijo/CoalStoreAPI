using API.ModelsDTO;
using API.Repositories.Models;
using API.ModelsDTO.ProductsDTO;

namespace API.Services.Interfaces
{
    public interface IProductsService
    {
        public ResultData CreateProducts(CreateProductsDTO ProductsDTO);
        public ResultData UpdateProducts(UpdateProductsDTO ProductsDTO);
        public ResultData DeleteProducts(int productsId);
        public Products GetProductByName(string productName);
        public List<ProductsWithCategory> GetProductsWithCategory();
        public List<Products> GetProducts();
        public ResultData CreateProductsCategoriesMapping(ProductsCategories productsCategories);
        public ResultData UpdateProductsCategoriesMapping(ProductsCategories productsCategories);
        public ResultData DeleteProductsCategoriesMapping(int Id);
    }

    public class ProductsWithCategory
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Id { get; set; }
    }
}
