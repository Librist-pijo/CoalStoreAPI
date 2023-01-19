
using API.ModelsDTO;
using API.ModelsDTO.OrdersProductsDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface IOrdersProductsService
    {
        public Task<ResultData> CreateOrdersProducts(CreateOrdersProductsDTO OrdersProductsDTO);
        public Task<ResultData> UpdateOrdersProducts(UpdateOrdersProductsDTO OrdersProductsDTO);
        public Task<ResultData> DeleteOrdersProducts(int orderId);
        public Task<List<OrdersProducts>> GetOrdersProductsById(int orderId);
        public Task<List<OrdersProducts>> GetOrdersProducts();
    }
}
