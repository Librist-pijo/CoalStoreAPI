using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IInvoicesRepository
    {
        Task Create(Invoices invoice);
        Task Update(Invoices invoice);
        Task Delete(Invoices invoice);
        Task<List<Invoices>> Get();
        Task<Invoices> GetById(int invoiceId);
    }
}
