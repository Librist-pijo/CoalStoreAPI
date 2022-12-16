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
        protected readonly IInvoicesRepository _InvoicesRepository;
        protected readonly IInvoicesValidator _InvoicesValidator;
        protected readonly InvoicesFactory _InvoicesFactory;

        public InvoicesService(IInvoicesRepository InvoicesRepository,
                                 IInvoicesValidator InvoicesValidator)
        {
            _InvoicesRepository = InvoicesRepository;
            _InvoicesValidator = InvoicesValidator;
            _InvoicesFactory = new InvoicesFactory();
        }

        public ResultData CreateInvoices(CreateInvoicesDTO InvoicesDTO)
        {
            ResultData result = new();

            try
            {
                Invoices Invoices = _InvoicesFactory.CreateInvoices(InvoicesDTO);
                var validation = _InvoicesValidator.ValidateCreateAsync(Invoices).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _InvoicesRepository.Create(Invoices);
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
                Invoices Invoices = _InvoicesFactory.UpdateInvoices(InvoicesDTO);
                var validation = _InvoicesValidator.ValidateUpdateAsync(Invoices).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _InvoicesRepository.Update(Invoices);
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
                var validation = _InvoicesValidator.ValidateDeleteAsync(invoiceId).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _InvoicesRepository.Delete(invoiceId);
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
            var invoice = _InvoicesRepository.GetById(invoiceId).GetAwaiter().GetResult();
            return invoice;
        }
        public List<Invoices> Get()
        {
            var invoice = _InvoicesRepository.Get().GetAwaiter().GetResult();
            return invoice;
        }
    }
}
