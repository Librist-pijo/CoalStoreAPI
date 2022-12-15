using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IProductsCategoriesValidator
    {
        public Task<bool> ValidateCreateAsync(ProductsCategories productsCategories);
        public Task<bool> ValidateUpdateAsync(ProductsCategories productsCategories);
        public Task<bool> ValidateDeleteAsync(int Id);
    }
}
