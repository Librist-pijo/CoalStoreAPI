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
        public List<Products> GetProducts();
    }
}
