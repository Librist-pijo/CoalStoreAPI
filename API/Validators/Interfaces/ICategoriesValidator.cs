using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface ICategoriesValidator
    {
        public Task<bool> ValidateCreateAsync(Categories categories);
        public Task<bool> ValidateUpdateAsync(Categories categories);
        public Task<bool> ValidateDeleteAsync(int categoryId);
    }
}
