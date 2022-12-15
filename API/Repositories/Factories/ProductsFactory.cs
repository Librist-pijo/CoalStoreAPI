using API.ModelsDTO.ProductsDTO;
using API.Repositories.Models;

namespace API.Repositories.Factories
{
    public class ProductsFactory
    {
        public Products CreateProducts(CreateProductsDTO ProductsDTO)
        {
            Products newProduct = new Products
            {
                Name = ProductsDTO.Name,
                Stock= ProductsDTO.Stock,
                Price= ProductsDTO.Price
            };
            return newProduct;
        }
        public Products UpdateProducts(UpdateProductsDTO ProductsDTO)
        {
            Products newProduct = new Products
            {
                Id = ProductsDTO.Id,
                Name = ProductsDTO.Name,
                Stock= ProductsDTO.Stock,
                Price= ProductsDTO.Price
            };
            return newProduct;
        }
    }
}
