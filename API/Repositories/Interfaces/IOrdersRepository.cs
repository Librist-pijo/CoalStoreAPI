using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task CreatOrder(Orders order);
        Task UpdateOrder(Orders order);
        Task DeleteOrder(Orders order);
        Task GetOrders();
        Task GetOrderById(int orderId);
    }
}
