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
        public OrdersValidator(IOrdersRepository ordersRepository,
                               ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        public async Task<bool> ValidateCreateAsync(Orders orders)
        {
            bool customerValidation = await ValidateCustomer(orders);
            if (customerValidation)
            {
                var customer = await _customersRepository.GetCustomerById(orders.CustomerId);
                orders.ShippingAddress = $"{customer.AddressLine1}" +
                    $" {customer.AddressLine2}" +
                    $" {customer.PostCode}";
            }
            bool orderDateValidation = await ValidateOrderDate(orders);
            bool shippingDateValidation = await ValidateShippingAddress(orders);
            bool stateValidation = await ValidateState(orders);
            if (!orderDateValidation
                || !shippingDateValidation
                || !stateValidation)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(Orders orders)
        {
            bool existValidation = await ValidateIfExists(orders);
            if (existValidation)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUpdateAsync(Orders orders)
        {
            bool customerValidation = await ValidateCustomer(orders);
            if (customerValidation)
            {
                var customer = await _customersRepository.GetCustomerById(orders.CustomerId);
                orders.ShippingAddress = $"{customer.AddressLine1}" +
                    $" {customer.AddressLine2}" +
                    $" {customer.PostCode}";
            }
            bool orderDateValidation = await ValidateOrderDate(orders);
            bool shippingDateValidation = await ValidateShippingDate(orders);
            bool shippingAdressValidation = await ValidateShippingAddress(orders);
            bool stateValidation = await ValidateState(orders);
            if (!orderDateValidation
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
            if (orders.OrderDate > DateTimeOffset.UtcNow)
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
            if (orders.ShippingAddress.Length < 255)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateShippingDate(Orders orders)
        {
            if (orders.ShippingDate == null)
            {
                return false;
            }
            if (orders.ShippingDate > DateTimeOffset.UtcNow)
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
        private async Task<bool> ValidateIfExists(Orders orders)
        {
            var order = _ordersRepository.GetOrderById(orders.Id);
            if (order == null || order == default)
            {
                return false;
            }
            return true;
        }
    }
}
