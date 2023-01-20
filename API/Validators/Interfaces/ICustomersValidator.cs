using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface ICustomersValidator
    {
        public Task<ResultData> ValidateCreateAsync(Customers customers);
        public Task<ResultData> ValidateUpdateAsync(Customers customers);
        public Task<ResultData> ValidateDeleteAsync(Customers customers);
    }
}
