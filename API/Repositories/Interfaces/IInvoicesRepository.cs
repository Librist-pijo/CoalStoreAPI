using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IInvoicesRepository
    {
        Task Create(Invoices invoice);
        Task Update(Invoices invoice);
        Task Delete(int invoiceId);
        Task<List<Invoices>> Get();
        Task<Invoices> GetById(int invoiceId);
    }
}
