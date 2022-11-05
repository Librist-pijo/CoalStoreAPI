using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        Task CreateCustomer(Customers customer);
        Task UpdateCustomer(Customers customer);
        Task DeleteCustomer(Customers customer);
        Task GetCustomers();
        Task GetCustomerByLogin(string customerName);
        Task CheckIfEmailExists(string email);
    }
}
