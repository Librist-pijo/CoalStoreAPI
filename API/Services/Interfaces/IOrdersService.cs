using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.Orders;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface IOrdersService
    {
        public List<EnumDescriptionDTO<OrderState>> GetOrdersStates();
        public Task<ResultData> CreateOrders(CreateOrdersDTO ordersDTO);
        public Task<ResultData> UpdateOrders(UpdateOrdersDTO ordersDTO);
        public Task<ResultData> DeleteOrders(int orderId);
        public Task<Orders> GetOrderById(int orderId);
        public Task<List<Orders>> GetOrders();
    }
}
