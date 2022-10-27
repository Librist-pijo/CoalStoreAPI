using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task CreatProduct(Products product);
        Task UpdateProduct(Products product);
        Task DeleteProduct(Products product);
        Task GetProducts();
        Task GetProductByName(int productName);
    }
}
