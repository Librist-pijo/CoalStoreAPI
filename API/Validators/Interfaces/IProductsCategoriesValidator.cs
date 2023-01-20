using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IProductsCategoriesValidator
    {
        public Task<ResultData> ValidateCreateAsync(ProductsCategories productsCategories);
        public Task<ResultData> ValidateUpdateAsync(ProductsCategories productsCategories);
        public Task<ResultData> ValidateDeleteAsync(int Id);
    }
}
