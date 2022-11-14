using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IOrdersProducts
    {
        Task Creat(OrdersProducts ordersProducts);
        Task Update(OrdersProducts ordersProducts);
        Task Delete(OrdersProducts ordersProducts);
        Task<List<OrdersProducts>> Get();
        Task<List<OrdersProducts>> GetByOrderId(int orderId);
    }
}
