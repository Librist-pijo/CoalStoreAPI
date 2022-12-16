using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;

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


        public async Task<bool> ValidateCreateAsync(OrdersProducts ordersProducts)
        {
            var productsValidation = await ValidateProductsExists(ordersProducts);
            var ordersValidation = await ValidateOrdersExists(ordersProducts);
            var quantityValidation = await ValidateQuantity(ordersProducts);

            if (!productsValidation
                || !ordersValidation
                || !quantityValidation)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(int Id)
        {
            var ordersProductsValidation = await ValidateOrdersProductsExists(Id);

            if (!ordersProductsValidation)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateUpdateAsync(OrdersProducts ordersProducts)
        {
            var productsValidation = await ValidateProductsExists(ordersProducts);
            var ordersValidation = await ValidateOrdersExists(ordersProducts);
            var quantityValidation = await ValidateQuantity(ordersProducts);

            if (!productsValidation
                || !ordersValidation
                || quantityValidation)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateProductsExists(OrdersProducts ordersProducts)
        {
            var product = await _productsRepository.GetById(ordersProducts.ProductId);

            if (product == null || product == default)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateOrdersProductsExists(int orderId)
        {
            var product = await _ordersProductsRepository.GetByOrderId(orderId);

            if (product == null || product == default)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateOrdersExists(OrdersProducts ordersProducts)
        {
            var order = await _ordersRepository.GetById(ordersProducts.OrderId);

            if (order == null || order == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateQuantity(OrdersProducts ordersProducts)
        {
            var product = await _productsRepository.GetById(ordersProducts.ProductId);

            if (product == null || product == default)
            {
                return false;
            }

            if(product.Stock < ordersProducts.Quantity) 
            {
                return false;
            }

            return true;
        }

    }
}
