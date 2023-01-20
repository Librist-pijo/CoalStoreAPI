using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface ICategoriesValidator
    {
        public Task<ResultData> ValidateCreateAsync(Categories categories);
        public Task<ResultData> ValidateUpdateAsync(Categories categories);
        public Task<ResultData> ValidateDeleteAsync(int categoryId);
    }
}
