using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IInvoicesRepository
    {
        Task CreateInvoice(Invoices invoice);
        Task UpdateInvoice(Invoices invoice);
        Task DeleteInvoice(Invoices invoice);
        Task GetInvoices();
        Task GetInvoiceById(int invoiceId);
    }
}
