using API.Repositories.Interfaces;
using API.Repositories.Models;

namespace API.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        public Task CreateCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }

        public Task GetCustomerByLogin(string customerName)
        {
            throw new NotImplementedException();
        }

        public Task GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }
    }
}
