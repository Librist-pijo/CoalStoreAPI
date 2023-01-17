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

        public async Task<ResultData> CreateOrdersProducts(CreateOrdersProductsDTO ordersProductsDTO)
        {
            ResultData result = new();

            try
            {
                OrdersProducts ordersProducts = _ordersProductsFactory.CreateOrdersProducts(ordersProductsDTO);
                var validation = await _ordersProductsValidator.ValidateCreateAsync(ordersProducts);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _ordersProductsRepository.Create(ordersProducts);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ResultData> DeleteOrdersProducts(int orderId)
        {
            ResultData result = new();

            try
            {
                var validation = await _ordersProductsValidator.ValidateDeleteAsync(orderId);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _ordersProductsRepository.Delete(orderId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public async Task<List<OrdersProducts>> GetOrdersProducts()
        {
            return await _ordersProductsRepository.Get();
        }

        public async Task<List<OrdersProducts>> GetOrdersProductsById(int orderId)
        {
            return await _ordersProductsRepository.GetByOrderId(orderId);
        }

        public async Task<ResultData> UpdateOrdersProducts(UpdateOrdersProductsDTO ordersProductsDTO)
        {
            ResultData result = new();

            try
            {
                OrdersProducts ordersProducts = _ordersProductsFactory.UpdateOrdersProducts(ordersProductsDTO);
                var validation = await _ordersProductsValidator.ValidateUpdateAsync(ordersProducts);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _ordersProductsRepository.Update(ordersProducts);
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
