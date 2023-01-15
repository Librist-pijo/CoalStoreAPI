using API.ModelsDTO;
using API.ModelsDTO.CustomersDTO;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;

namespace API.Services
{
    public class CustomersService : ICustomersService
    {
        protected readonly ICustomersRepository _customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public Customers GetCustomerById(int id)
        {
            try
            {
                var customer = _customersRepository.GetCustomerById(id).GetAwaiter().GetResult();
                return customer;
            }
            catch(Exception ex)
            {
                return new Customers();
            }
        }
        public Customers GetCustomerByLogin(string login)
        {
            try
            {
                var customer = _customersRepository.GetCustomerByLogin(login).GetAwaiter().GetResult();
                return customer;
            }
            catch(Exception ex)
            {
                return new Customers();
            }
        }
        public ResultData UpdateCustomer(Customers customer)
        {
            ResultData resultData = new ResultData();

            try
            {
                _customersRepository.UpdateCustomer(customer).GetAwaiter().GetResult();
                resultData.Data = GetCustomerById(customer.Id);
                resultData.Success = true;
            }
            catch (Exception ex)
            {
                resultData.Success = false;
            }
            return resultData;
        }

        public ResultData UpdateCustomerPassword(UpdatePasswordDTO updatePasswordDTO)
        {
            ResultData resultData = new ResultData();

            try
            {
                var customer = _customersRepository
                    .GetCustomerByLoginAndPassword(updatePasswordDTO.CustomerLogin, updatePasswordDTO.OldPassword).GetAwaiter().GetResult();

                if (customer == null || customer.Id == 0)
                {
                    resultData.Error = "Podane aktualne hasło jest błędne";
                }
                else
                {
                    _customersRepository.UpdateCustomerPassword(updatePasswordDTO).GetAwaiter().GetResult();
                    resultData.Data = GetCustomerById(updatePasswordDTO.CustomerId);
                    resultData.Success = true;
                }
            }
            catch(Exception ex)
            {
                resultData.Success = false;
            }
            return resultData;
        }
    }
}
