using API.Enums;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System;
using System.Linq;

namespace API.Validators
{
    public class OrdersValidator : IOrdersValidator
    {
        protected readonly IOrdersRepository _ordersRepository;
        protected readonly ICustomersRepository _customersRepository;
        private static int MaxLength = 255;
        public OrdersValidator(IOrdersRepository ordersRepository,
                               ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        public async Task<bool> ValidateCreateAsync(Orders orders)
        {
            bool customerValidation = await ValidateCustomer(orders);
            bool orderDateValidation = await ValidateOrderDate(orders);
            bool stateValidation = await ValidateState(orders);
            if (!orderDateValidation
                || !stateValidation
                || !customerValidation)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(int orderId)
        {
            bool orderExistsValidation = await ValidateIfExists(orderId);
            if (orderExistsValidation)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUpdateAsync(Orders orders)
        {
            bool orderExistsValidation = await ValidateIfExists(orders.Id);
            bool shippingDateValidation = await ValidateShippingDate(orders,false);
            bool shippingAdressValidation = await ValidateShippingAddress(orders);
            bool stateValidation = await ValidateState(orders);
            if (!orderExistsValidation
                || !shippingDateValidation
                || !shippingAdressValidation
                || !stateValidation)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateCustomer(Orders orders)
        {
            var customer = await _customersRepository.GetCustomerById(orders.CustomerId);

            if (customer == null || customer == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateOrderDate(Orders orders)
        {
            if (orders.OrderDate == null)
            {
                return false;
            }
            if (orders.OrderDate > DateTimeOffset.UtcNow.AddMinutes(-1))
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateShippingAddress(Orders orders)
        {
            if (orders.ShippingAddress == null)
            {
                return false;
            }
            if (orders.ShippingAddress.Length <= MaxLength)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateShippingDate(Orders orders, bool creation)
        {
            if (orders.ShippingDate == null && !creation)
            {
                return false;
            }
            if (orders.ShippingDate == null && creation)
            {
                return true;
            }
            if (orders.ShippingDate > DateTimeOffset.UtcNow.AddMinutes(-1))
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateState(Orders orders)
        {
            HashSet<int> validVals = new HashSet<int>((int[])Enum.GetValues(typeof(OrderState)));
            if (validVals.Contains(orders.State))
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateIfExists(int ordersId)
        {
            var order = _ordersRepository.GetById(ordersId);
            if (order == null || order == default)
            {
                return false;
            }
            return true;
        }
    }
}
