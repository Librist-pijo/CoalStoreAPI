using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IOrdersProductsRepository
    {
        Task Create(OrdersProducts ordersProducts);
        Task Update(OrdersProducts ordersProducts);
        Task Delete(int Id);
        Task<List<OrdersProducts>> Get();
        Task<List<OrdersProducts>> GetByOrderId(int orderId);
    }
}
