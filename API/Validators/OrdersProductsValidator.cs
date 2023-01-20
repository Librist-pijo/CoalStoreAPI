using API.ModelsDTO;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace API.Validators
{
    public class OrdersProductsValidator : IOrdersProductsValidator
    {
        protected readonly IOrdersRepository _ordersRepository;
        protected readonly IProductsRepository _productsRepository;
        protected readonly IOrdersProductsRepository _ordersProductsRepository;

        public OrdersProductsValidator(IOrdersRepository ordersRepository,
                                       IProductsRepository productsRepository,
                                       IOrdersProductsRepository ordersProductsRepository)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _ordersProductsRepository = ordersProductsRepository;
        }


        public async Task<ResultData> ValidateCreateAsync(OrdersProducts ordersProducts)
        {
            var productsValidation = await ValidateProductsExists(ordersProducts);
            var ordersValidation = await ValidateOrdersExists(ordersProducts);
            var quantityValidation = await ValidateQuantity(ordersProducts);

            if (!productsValidation.Success)
            {
                return productsValidation;
            }
            if (!ordersValidation.Success)
            {
                return ordersValidation;
            }
            if (!quantityValidation.Success)
            {
                return quantityValidation;
            }
            return new ResultData { Success = true };
        }

        public async Task<ResultData> ValidateDeleteAsync(int Id)
        {
            var ordersProductsValidation = await ValidateOrdersProductsExists(Id);

            if (!ordersProductsValidation.Success)
            {
                return ordersProductsValidation;
            }
            return ordersProductsValidation;
        }

        public async Task<ResultData> ValidateUpdateAsync(OrdersProducts ordersProducts)
        {
            var productsValidation = await ValidateProductsExists(ordersProducts);
            var ordersValidation = await ValidateOrdersExists(ordersProducts);
            var quantityValidation = await ValidateQuantity(ordersProducts);

            if (!productsValidation.Success)
            {
                return productsValidation;
            }
            if (!ordersValidation.Success)
            {
                return ordersValidation;
            }
            if (!quantityValidation.Success)
            {
                return quantityValidation;
            }
            return new ResultData { Success = true };
        }

        private async Task<ResultData> ValidateProductsExists(OrdersProducts ordersProducts)
        {
            var validationResult = new ResultData();
            var product = await _productsRepository.GetById(ordersProducts.ProductId);

            if (product == null || product == default)
            {
                validationResult.Error = $"Product with id: {ordersProducts.ProductId} do not exists in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }

        private async Task<ResultData> ValidateOrdersProductsExists(int orderId)
        {
            var validationResult = new ResultData();
            var product = await _ordersProductsRepository.GetByOrderId(orderId);

            if (product == null || product == default)
            {
                validationResult.Error = $"Order with id: {orderId} do not exists in database";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }

        private async Task<ResultData> ValidateOrdersExists(OrdersProducts ordersProducts)
        {
            var validationResult = new ResultData();
            var order = await _ordersRepository.GetById(ordersProducts.OrderId);

            if (order == null || order == default)
            {
                validationResult.Error = $"Products on order with id: {ordersProducts.OrderId} do not exists in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }
        private async Task<ResultData> ValidateQuantity(OrdersProducts ordersProducts)
        {
            var validationResult = new ResultData();
            var product = await _productsRepository.GetById(ordersProducts.ProductId);

            if (product == null || product == default)
            {
                validationResult.Error = $"Quantity cannot be null";
                validationResult.Success = false;
                return validationResult;
            }

            if(product.Stock < ordersProducts.Quantity) 
            {
                validationResult.Error = $"Not enough product in stock";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }

    }
}
