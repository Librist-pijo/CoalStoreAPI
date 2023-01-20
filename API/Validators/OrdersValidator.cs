using API.Enums;
using API.ModelsDTO;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
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

        public async Task<ResultData> ValidateCreateAsync(Orders orders)
        {
            var customerValidation = await ValidateCustomer(orders);
            var orderDateValidation = await ValidateOrderDate(orders);
            var stateValidation = await ValidateState(orders);
            if (!orderDateValidation.Success)
            {
                return orderDateValidation;
            }
            if (!stateValidation.Success)
            {
                return stateValidation;
            }
            if (!customerValidation.Success)
            {
                return customerValidation;
            }
            return new ResultData { Success = true };
        }

        public async Task<ResultData> ValidateDeleteAsync(int orderId)
        {
            return await ValidateIfExists(orderId);
        }

        public async Task<ResultData> ValidateUpdateAsync(Orders orders)
        {
            var orderExistsValidation = await ValidateIfExists(orders.Id);
            var shippingDateValidation = await ValidateShippingDate(orders,false);
            var shippingAdressValidation = await ValidateShippingAddress(orders);
            var stateValidation = await ValidateState(orders);
            if (!orderExistsValidation.Success)
            {
                return orderExistsValidation;
            }
            if (!shippingAdressValidation.Success)
            {
                return shippingAdressValidation;
            }  
            if (!shippingDateValidation.Success)
            {
                return shippingDateValidation;
            }
            if (!stateValidation.Success)
            {
                return stateValidation;
            }

            return new ResultData { Success = true };
        }

        private async Task<ResultData> ValidateCustomer(Orders orders)
        {
            var validationResult = new ResultData();
            var customer = await _customersRepository.GetCustomerById(orders.CustomerId);

            if (customer == null || customer == default)
            {
                validationResult.Error = $"There is no customer with id: {orders.CustomerId} in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }
        private async Task<ResultData> ValidateOrderDate(Orders orders)
        {
            var validationResult = new ResultData();
            if (orders.OrderDate == null)
            {
                validationResult.Error = $"Date cannot be null";
                validationResult.Success = false;
                return validationResult;
            }
            if (orders.OrderDate > DateTimeOffset.UtcNow.AddMinutes(-1))
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Date need to be current: {orders.OrderDate}";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateShippingAddress(Orders orders)
        {
            var validationResult = new ResultData();
            if (orders.ShippingAddress == null)
            {
                validationResult.Error = $"ShippingAddress cannot be null";
                validationResult.Success = false;
                return validationResult;
            }
            if (orders.ShippingAddress.Length <= MaxLength)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"ShippingAddress cannot exceed: {MaxLength}";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateShippingDate(Orders orders, bool creation)
        {
            var validationResult = new ResultData();
            if (orders.ShippingDate == null && !creation)
            {
                validationResult.Error = $"ShippingData cannot be null";
                validationResult.Success = false;
                return validationResult;
            }
            if (orders.ShippingDate == null && creation)
            {
                validationResult.Success = true;
                return validationResult;
            }
            if (orders.ShippingDate > DateTimeOffset.UtcNow.AddMinutes(-1))
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"ShippingDate need to be current: {orders.ShippingDate}";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateState(Orders orders)
        {
            var validationResult = new ResultData();
            HashSet<int> validVals = new HashSet<int>((int[])Enum.GetValues(typeof(OrderState)));
            if (validVals.Contains(orders.State))
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Wrong state: {orders.State}";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateIfExists(int ordersId)
        {
            var validationResult = new ResultData();
            var order = _ordersRepository.GetById(ordersId);
            if (order == null || order == default)
            {
                validationResult.Error = $"There is no order with order id: {ordersId} in database";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }
    }
}
