using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.Orders;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface IOrdersService
    {
        public List<EnumDescriptionDTO<OrderState>> GetOrdersStates();
        public ResultData CreateOrders(CreateOrdersDTO ordersDTO);
        public ResultData UpdateOrders(UpdateOrdersDTO ordersDTO);
        public ResultData DeleteOrders(int orderId);
        public Orders GetOrderById(int orderId);
        public List<Orders> GetOrders();
    }
}
