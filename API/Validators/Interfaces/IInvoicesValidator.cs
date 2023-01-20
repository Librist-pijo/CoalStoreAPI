using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface IInvoicesValidator
    {
        public Task<ResultData> ValidateCreateAsync(Invoices invoices);
        public Task<ResultData> ValidateUpdateAsync(Invoices invoices);
        public Task<ResultData> ValidateDeleteAsync(int invoicesId);
    }
}
