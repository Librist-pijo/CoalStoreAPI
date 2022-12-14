using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IInvoicesValidator
    {
        public Task<bool> ValidateCreateAsync(Invoices invoices);
        public Task<bool> ValidateUpdateAsync(Invoices invoices);
        public Task<bool> ValidateDeleteAsync(Invoices invoices);
    }
}
