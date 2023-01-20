using API.ModelsDTO;
using API.ModelsDTO.InvoicesDTO;
using API.Repositories.Factories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using API.Validators.Interfaces;

namespace API.Services
{
    public class InvoicesService : IInvoicesService
    {
        protected readonly IInvoicesRepository _invoicesRepository;
        protected readonly IInvoicesValidator _invoicesValidator;
        protected readonly InvoicesFactory _invoicesFactory;

        public InvoicesService(IInvoicesRepository InvoicesRepository,
                                 IInvoicesValidator InvoicesValidator)
        {
            _invoicesRepository = InvoicesRepository;
            _invoicesValidator = InvoicesValidator;
            _invoicesFactory = new InvoicesFactory();
        }

        public async Task<ResultData> CreateInvoices(CreateInvoicesDTO InvoicesDTO)
        {
            ResultData result = new();

            try
            {
                Invoices Invoices = _invoicesFactory.CreateInvoices(InvoicesDTO);
                var validation = await _invoicesValidator.ValidateCreateAsync(Invoices);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                _invoicesRepository.Create(Invoices);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }
        public async Task<ResultData> UpdateInvoices(UpdateInvoicesDTO InvoicesDTO)
        {
            ResultData result = new();

            try
            {
                Invoices Invoices = _invoicesFactory.UpdateInvoices(InvoicesDTO);
                var validation = await _invoicesValidator.ValidateUpdateAsync(Invoices);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                _invoicesRepository.Update(Invoices);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ResultData> DeleteInvoices(int invoiceId)
        {
            ResultData result = new();

            try
            {
                var validation = await _invoicesValidator.ValidateDeleteAsync(invoiceId);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                _invoicesRepository.Delete(invoiceId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }
        public async Task<Invoices> GetById(int invoiceId)
        {
            var invoice = await _invoicesRepository.GetById(invoiceId);
            return invoice;
        }
        public async Task<Invoices> GetByOrderId(int orderId)
        {
            var invoice = await _invoicesRepository.GetByOrderId(orderId);
            return invoice;
        }
        public async Task<List<Invoices>> Get()
        {
            var invoice = await _invoicesRepository.Get();
            return invoice;
        }
    }
}
