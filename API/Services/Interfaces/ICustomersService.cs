using API.ModelsDTO;
using API.ModelsDTO.CustomersDTO;
using API.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface ICustomersService 
    {
        Customers GetCustomerById(int id);
        Customers GetCustomerByLogin(string login);
        ResultData UpdateCustomer(Customers customer);
        ResultData UpdateCustomerPassword(UpdatePasswordDTO updatePasswordDTO);
    }
}
