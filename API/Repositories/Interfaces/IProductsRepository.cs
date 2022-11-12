using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task CreateProduct(Products product);
        Task UpdateProduct(Products product);
        Task DeleteProduct(Products product);
        Task<List<Products>> GetProducts();
        Task<Products> GetProductByName(string productName);
    }
}
