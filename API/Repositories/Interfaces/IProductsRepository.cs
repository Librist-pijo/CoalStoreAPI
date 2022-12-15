using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task Create(Products product);
        Task Update(Products product);
        Task Delete(int productId);
        Task<List<Products>> Get();
        Task<Products> GetByName(string productName);
        Task<Products> GetById(int productId);
    }
}
