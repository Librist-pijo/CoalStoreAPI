using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess.Interfaces;
using System.Data;

namespace API.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IDataAccess _dataAccess;

        public CustomersRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task CreateCustomer(Customers customer)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", customer.Login);
            parameters.Add("Password", customer.Password);
            parameters.Add("FirstName", customer.FirstName);
            parameters.Add("LastName", customer.LastName);
            parameters.Add("AddressLine1", customer.AddressLine1);
            parameters.Add("AddressLine2", customer.AddressLine2);
            parameters.Add("PostCode", customer.PostCode);
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCreateCustomer", parameters, "SQLDB");
        }

        public Task DeleteCustomer(Customers customer)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("Id", customer.Id);

            return _dataAccess.SaveData("dbo.spDeleteCustomer", parameter, "SQLDB");
        }

        public async Task<Customers> GetCustomerByLogin(string customerLogin)
        {
            var Customer = await _dataAccess.LoadData<Customers, dynamic>
                ("dbo.spGetCustomerByLogin",
                new
                {
                    Name = customerLogin
                },
                "SQLDB");

            return Customer.FirstOrDefault();
        }

        public async Task<Customers> GetCustomerById(int customerId)
        {
            var Customer = await _dataAccess.LoadData<Customers, dynamic>
                ("dbo.spGetCustomerById",
                new
                {
                    Id = customerId
                },
                "SQLDB");

            return Customer.FirstOrDefault();
        }

        public async Task<List<Customers>> GetCustomers()
        {
            return await _dataAccess.LoadData<Customers, dynamic>
              ("dbo.spGetCustomers",
              new
              { },
              "SQLDB");
        }

        public async Task UpdateCustomer(Customers customer)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", customer.Login);
            parameters.Add("Password", customer.Password);
            parameters.Add("FirstName", customer.FirstName);
            parameters.Add("LastName", customer.LastName);
            parameters.Add("AddressLine1", customer.AddressLine1);
            parameters.Add("AddressLine2", customer.AddressLine2);
            parameters.Add("PostCode", customer.PostCode);
            parameters.Add("Id", customer.Id);

            await _dataAccess.SaveData("dbo.spUpdateCustomer", parameters, "SQLDB");
        }

        //public Task CheckIfEmailExists(string email)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
