using API.Repositories.Interfaces;
using API.Repositories.Models;

namespace API.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public Task CreatOrder(Orders order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrder(Orders order)
        {
            throw new NotImplementedException();
        }

        public Task GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(Orders order)
        {
            throw new NotImplementedException();
        }
    }
}
