using API.ModelsDTO.Orders;
using API.Repositories.Models;

namespace API.Repositories.Factories
{
    public class OrdersFactory
    {
        public Orders CreateOrders(CreateOrdersDTO ordersDTO)
        {
            Orders newOrder = new Orders
            {
                CustomerId= ordersDTO.CustomerId,
                OrderDate = DateTimeOffset.UtcNow,
                State= ordersDTO.State
            };
            return newOrder;
        }
        public Orders UpdateOrders(UpdateOrdersDTO ordersDTO)
        {
            Orders newOrder = new Orders
            {
                Id = ordersDTO.Id,
                State = ordersDTO.State,
                ShippingAddress = ordersDTO.ShippingAddress
            };
            return newOrder;
        }
    }
}
