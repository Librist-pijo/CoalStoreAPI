using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IOrdersValidator
    {
        public Task<ResultData> ValidateCreateAsync(Orders orders);
        public Task<ResultData> ValidateUpdateAsync(Orders orders);
        public Task<ResultData> ValidateDeleteAsync(int orderId);
    }
}
