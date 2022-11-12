using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        Task CreateCustomer(Customers customer);
        Task UpdateCustomer(Customers customer);
        Task DeleteCustomer(Customers customer);
        Task<List<Customers>> GetCustomers();
        Task<Customers> GetCustomerByLogin(string customerLogin);
        //Task CheckIfEmailExists(string email);
    }
}
