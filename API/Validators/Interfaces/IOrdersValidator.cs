using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IOrdersValidator
    {
        public Task<bool> ValidateCreateAsync(Orders orders);
        public Task<bool> ValidateUpdateAsync(Orders orders);
        public Task<bool> ValidateDeleteAsync(int orderId);
    }
}
