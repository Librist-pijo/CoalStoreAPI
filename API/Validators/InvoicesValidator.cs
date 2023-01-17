using API.Enums;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;

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

        public async Task<bool> ValidateCreateAsync(Invoices invoices)
        {
            bool orderValidation = await ValidateOrderExists(invoices);
            if (!orderValidation)
            {
                return false;
            }
            bool paymentMethodValidation = await ValidatePaymentMethod(invoices);
            bool stateValidation = await ValidateState(invoices);
            bool amountValidation = await ValidateAmount(invoices);
            if (!paymentMethodValidation
                || !stateValidation
                || !amountValidation)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateDeleteAsync(int invoicesId)
        {
            bool invoiceValidation = await ValidateInvoiceExists(invoicesId);
            if (!invoiceValidation)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateUpdateAsync(Invoices invoices)
        {
            bool orderValidation = await ValidateOrderExists(invoices);
            if (!orderValidation)
            {
                return false;
            }
            bool paymentMethodValidation = await ValidatePaymentMethod(invoices);
            bool stateValidation = await ValidateState(invoices);
            bool amountValidation = await ValidateAmount(invoices);
            if (!paymentMethodValidation
                || !stateValidation
                || !amountValidation)
            {
                return false;
            }
            return true;
        }
        private async Task<bool> ValidateOrderExists(Invoices invoices)
        {
            var order = await _ordersRepository.GetById(invoices.OrderId);

            if (order == null || order == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateInvoiceExists(int orderId)
        {
            var invoice = await _invoicesRepository.GetByOrderId(orderId);

            if (invoice == null || invoice == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidatePaymentMethod(Invoices invoices)
        {
            HashSet<int> validVals = new HashSet<int>((int[])Enum.GetValues(typeof(PaymentMethod)));
            if (validVals.Contains(invoices.PaymentMethod))
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateState(Invoices invoices)
        {
            HashSet<int> validVals = new HashSet<int>((int[])Enum.GetValues(typeof(InvoiceState)));
            if (validVals.Contains(invoices.State))
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateAmount(Invoices invoices)
        {
            if (invoices.Amount > MinAmountNumber)
            {
                return true;
            }
            return false;
        }
    }
}
