using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IOrdersProductsValidator
    {
        public Task<bool> ValidateCreateAsync(OrdersProducts ordersProducts);
        public Task<bool> ValidateUpdateAsync(OrdersProducts ordersProducts);
        public Task<bool> ValidateDeleteAsync(int Id);
    }
}
