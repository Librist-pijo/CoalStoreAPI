using API.ModelsDTO.Orders;
using API.ModelsDTO;
using API.Repositories.Models;
using API.ModelsDTO.InvoicesDTO;

namespace API.Services.Interfaces
{
    public interface IInvoicesService
    {
        public Task<ResultData> CreateInvoices(CreateInvoicesDTO invoices);
        public Task<ResultData> UpdateInvoices(UpdateInvoicesDTO invoices);
        public Task<ResultData> DeleteInvoices(int orderId);
        public Task<List<Invoices>> Get();
        public Task<Invoices> GetById(int invoiceId);
        public Task<Invoices> GetByOrderId(int orderId);
    }
}
