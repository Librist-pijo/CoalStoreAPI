using API.Repositories.Models;

namespace API.Validators.Interfaces
{
    public interface ICustomersValidator
    {
        public Task<bool> ValidateCreateAsync(Customers customers);
        public Task<bool> ValidateUpdateAsync(Customers customers);
        public Task<bool> ValidateDeleteAsync(Customers customers);
    }
}
