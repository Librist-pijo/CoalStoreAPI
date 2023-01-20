using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IOrdersProductsValidator
    {
        public Task<ResultData> ValidateCreateAsync(OrdersProducts ordersProducts);
        public Task<ResultData> ValidateUpdateAsync(OrdersProducts ordersProducts);
        public Task<ResultData> ValidateDeleteAsync(int Id);
    }
}
