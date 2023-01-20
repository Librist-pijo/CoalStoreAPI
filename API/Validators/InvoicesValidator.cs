using API.Enums;
using API.ModelsDTO;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace API.Validators
{
    public class InvoicesValidator : IInvoicesValidator
    {
        protected readonly IInvoicesRepository _invoicesRepository;
        protected readonly IOrdersRepository _ordersRepository;

        const int MinAmountNumber = 0;
        public InvoicesValidator(IInvoicesRepository invoicesRepository,
                                 IOrdersRepository ordersRepository)
        {
            _invoicesRepository = invoicesRepository;
            _ordersRepository = ordersRepository;
        }

        public async Task<ResultData> ValidateCreateAsync(Invoices invoices)
        {
            var orderValidation = await ValidateOrderExists(invoices);
            if (!orderValidation.Success)
            {
                return orderValidation;
            }
            var paymentMethodValidation = await ValidatePaymentMethod(invoices);
            var stateValidation = await ValidateState(invoices);
            var amountValidation = await ValidateAmount(invoices);
            if (!paymentMethodValidation.Success)
            {
                return paymentMethodValidation;
            }
            if (!stateValidation.Success)
            {
                return stateValidation;
            }
            if (!amountValidation.Success)
            {
                return amountValidation;
            }
            return new ResultData { Success = true };
        }
        public async Task<ResultData> ValidateDeleteAsync(int invoicesId)
        {
            return await ValidateInvoiceExists(invoicesId);
        }
        public async Task<ResultData> ValidateUpdateAsync(Invoices invoices)
        {
            var orderValidation = await ValidateOrderExists(invoices);
            if (!orderValidation.Success)
            {
                return orderValidation;
            }
            var paymentMethodValidation = await ValidatePaymentMethod(invoices);
            var stateValidation = await ValidateState(invoices);
            var amountValidation = await ValidateAmount(invoices);
            if (!paymentMethodValidation.Success)
            {
                return paymentMethodValidation;
            }
            if (!stateValidation.Success)
            {
                return stateValidation;
            }
            if (!amountValidation.Success)
            {
                return amountValidation;
            }
            return new ResultData { Success = true };
        }
        private async Task<ResultData> ValidateOrderExists(Invoices invoices)
        {
            var validationResult = new ResultData();
            var order = await _ordersRepository.GetById(invoices.OrderId);

            if (order == null || order == default)
            {
                validationResult.Error = $"There is no order with id: {invoices.OrderId} in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }
        private async Task<ResultData> ValidateInvoiceExists(int orderId)
        {
            var validationResult = new ResultData();
            var invoice = await _invoicesRepository.GetByOrderId(orderId);

            if (invoice == null || invoice == default)
            {
                validationResult.Error = $"There is no invoice for order with id: {orderId} in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }
        private async Task<ResultData> ValidatePaymentMethod(Invoices invoices)
        {
            var validationResult = new ResultData();
            HashSet<int> validVals = new HashSet<int>((int[])Enum.GetValues(typeof(PaymentMethod)));
            if (validVals.Contains(invoices.PaymentMethod))
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Wrong payment method: {invoices.PaymentMethod}";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateState(Invoices invoices)
        {
            var validationResult = new ResultData();
            HashSet<int> validVals = new HashSet<int>((int[])Enum.GetValues(typeof(InvoiceState)));
            if (validVals.Contains(invoices.State))
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Wrong state: {invoices.State}";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateAmount(Invoices invoices)
        {
            var validationResult = new ResultData();
            if (invoices.Amount > MinAmountNumber)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Amount cannot be less than: {MinAmountNumber}";
            validationResult.Success = false;
            return validationResult;
        }
    }
}
