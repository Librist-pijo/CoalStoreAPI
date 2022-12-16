
using API.ModelsDTO;
using API.ModelsDTO.OrdersProductsDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface IOrdersProductsService
    {
        public ResultData CreateOrdersProducts(CreateOrdersProductsDTO OrdersProductsDTO);
        public ResultData UpdateOrdersProducts(UpdateOrdersProductsDTO OrdersProductsDTO);
        public ResultData DeleteOrdersProducts(int orderId);
        public List<OrdersProducts> GetOrdersProductsById(int orderId);
        public List<OrdersProducts> GetOrdersProducts();
    }
}
