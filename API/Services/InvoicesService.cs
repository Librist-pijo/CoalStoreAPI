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

        public ResultData CreateInvoices(CreateInvoicesDTO InvoicesDTO)
        {
            ResultData result = new();

            try
            {
                Invoices Invoices = _invoicesFactory.CreateInvoices(InvoicesDTO);
                var validation = _invoicesValidator.ValidateCreateAsync(Invoices).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
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
        public ResultData UpdateInvoices(UpdateInvoicesDTO InvoicesDTO)
        {
            ResultData result = new();

            try
            {
                Invoices Invoices = _invoicesFactory.UpdateInvoices(InvoicesDTO);
                var validation = _invoicesValidator.ValidateUpdateAsync(Invoices).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
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

        public ResultData DeleteInvoices(int invoiceId)
        {
            ResultData result = new();

            try
            {
                var validation = _invoicesValidator.ValidateDeleteAsync(invoiceId).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
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
        public Invoices GetById(int invoiceId)
        {
            var invoice = _invoicesRepository.GetById(invoiceId).GetAwaiter().GetResult();
            return invoice;
        }
        public Invoices GetByOrderId(int orderId)
        {
            var invoice = _invoicesRepository.GetByOrderId(orderId).GetAwaiter().GetResult();
            return invoice;
        }
        public List<Invoices> Get()
        {
            var invoice = _invoicesRepository.Get().GetAwaiter().GetResult();
            return invoice;
        }
    }
}
