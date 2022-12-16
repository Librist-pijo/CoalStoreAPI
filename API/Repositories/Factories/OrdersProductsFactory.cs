using API.ModelsDTO.Orders;
using API.ModelsDTO.OrdersProductsDTO;
using API.Repositories.Models;

namespace API.Repositories.Factories
{
    public class OrdersProductsFactory
    {
        public OrdersProducts CreateOrdersProducts(CreateOrdersProductsDTO ordersProductsDTO)
        {
            OrdersProducts newOrder = new OrdersProducts
            {
                OrderId = ordersProductsDTO.OrderId,
                ProductId = ordersProductsDTO.ProductId,
                Quantity= ordersProductsDTO.Quantity
            };
            return newOrder;
        }
        public OrdersProducts UpdateOrdersProducts(UpdateOrdersProductsDTO ordersProductsDTO)
        {
            OrdersProducts newOrder = new OrdersProducts
            {
                Id = ordersProductsDTO.Id,
                ProductId= ordersProductsDTO.ProductId,
                Quantity= ordersProductsDTO.Quantity,
                OrderId= ordersProductsDTO.OrderId
            };
            return newOrder;
        }
    }
}
