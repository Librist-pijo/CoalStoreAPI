using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IProductsValidator
    {
        public Task<bool> ValidateCreateAsync(Products products);
        public Task<bool> ValidateUpdateAsync(Products products);
        public Task<bool> ValidateDeleteAsync(int productsId);
    }
}
