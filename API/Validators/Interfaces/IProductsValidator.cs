using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IProductsValidator
    {
        public Task<ResultData> ValidateCreateAsync(Products products);
        public Task<ResultData> ValidateUpdateAsync(Products products);
        public Task<ResultData> ValidateDeleteAsync(int productsId);
    }
}
