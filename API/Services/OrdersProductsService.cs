using API.ModelsDTO;
using API.ModelsDTO.OrdersProductsDTO;
using API.Repositories;
using API.Repositories.Factories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using API.Validators;
using API.Validators.Interfaces;

namespace API.Services
{
    public class OrdersProductsService : IOrdersProductsService
    {
        protected readonly IOrdersProductsRepository _ordersProductsRepository;
        protected readonly IOrdersProductsValidator _ordersProductsValidator;
        protected readonly OrdersProductsFactory _ordersProductsFactory;

        public OrdersProductsService(IOrdersProductsRepository ordersProductsRepository,
                                     IOrdersProductsValidator ordersProductsValidator)
        {
            _ordersProductsRepository = ordersProductsRepository;
            _ordersProductsValidator = ordersProductsValidator;
            _ordersProductsFactory = new OrdersProductsFactory();
        }

        public ResultData CreateOrdersProducts(CreateOrdersProductsDTO ordersProductsDTO)
        {
            ResultData result = new();

            try
            {
                OrdersProducts ordersProducts = _ordersProductsFactory.CreateOrdersProducts(ordersProductsDTO);
                var validation = _ordersProductsValidator.ValidateCreateAsync(ordersProducts).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _ordersProductsRepository.Create(ordersProducts);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public ResultData DeleteOrdersProducts(int orderId)
        {
            ResultData result = new();

            try
            {
                var validation = _ordersProductsValidator.ValidateDeleteAsync(orderId).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _ordersProductsRepository.Delete(orderId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public List<OrdersProducts> GetOrdersProducts()
        {
            return _ordersProductsRepository.Get().GetAwaiter().GetResult();
        }

        public List<OrdersProducts> GetOrdersProductsById(int orderId)
        {
            return _ordersProductsRepository.GetByOrderId(orderId).GetAwaiter().GetResult();
        }

        public ResultData UpdateOrdersProducts(UpdateOrdersProductsDTO ordersProductsDTO)
        {
            ResultData result = new();

            try
            {
                OrdersProducts ordersProducts = _ordersProductsFactory.UpdateOrdersProducts(ordersProductsDTO);
                var validation = _ordersProductsValidator.ValidateUpdateAsync(ordersProducts).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _ordersProductsRepository.Update(ordersProducts);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }
    }
}
