using API.ModelsDTO.Orders;
using API.ModelsDTO;
using API.Repositories.Models;
using API.ModelsDTO.InvoicesDTO;

namespace API.Services.Interfaces
{
    public interface IInvoicesService
    {
        public ResultData CreateInvoices(CreateInvoicesDTO invoices);
        public ResultData UpdateInvoices(UpdateInvoicesDTO invoices);
        public ResultData DeleteInvoices(int orderId);
        public List<Invoices> Get();
        public Invoices GetById(int invoiceId);
    }
}
