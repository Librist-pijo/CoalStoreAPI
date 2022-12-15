using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.Orders;
using API.Repositories;
using API.Repositories.Factories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using API.Validators.Interfaces;

namespace API.Services
{
    public class OrdersService : IOrdersService
    {
        protected readonly IOrdersRepository _ordersRepository;
        protected readonly IOrdersValidator _ordersValidator;
        protected readonly IProductsRepository _productsRepository;
        protected readonly IOrdersProductsRepository _ordersProductsRepository;
        protected readonly OrdersFactory _ordersFactory;
        protected readonly ProductsFactory _productsFactory;

        public OrdersService(IOrdersRepository OrdersRepository,
                             IOrdersValidator OrdersValidator,
                             IProductsRepository productsRepository,
                             IOrdersProductsRepository ordersProductsRepository)
        {
            _ordersRepository = OrdersRepository;
            _ordersValidator = OrdersValidator;
            _ordersFactory = new OrdersFactory();
            _productsFactory = new ProductsFactory();
            _productsRepository = productsRepository;
            _ordersProductsRepository = ordersProductsRepository;
        }

        public ResultData CreateOrders(CreateOrdersDTO ordersDTO)
        {
            throw new NotImplementedException();
        }

        public ResultData DeleteOrders(int orderId)
        {
            throw new NotImplementedException();
        }

        public Orders GetOrderById(int orderId)
        {
            var order = _ordersRepository.GetById(orderId).GetAwaiter().GetResult();
            return order;
        }

        public List<Orders> GetOrders()
        {
            var order = _ordersRepository.Get().GetAwaiter().GetResult();
            return order;
        }

        public List<EnumDescriptionDTO<OrderState>> GetOrdersStates()
        {
            try
            {
                List<EnumDescriptionDTO<OrderState>> result = EnumService<OrderState>.GetEnumValues();
                return result;
            }
            catch (Exception ex)
            {
                return new List<EnumDescriptionDTO<OrderState>>();
            }
        }

        public ResultData UpdateOrders(UpdateOrdersDTO ordersDTO)
        {
            throw new NotImplementedException();
        }
    }
}
