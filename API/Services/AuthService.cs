using API.ModelsDTO;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using DataLibrary.DataAccess.Interfaces;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDataAccess _dataAccess;
        private readonly ICustomersRepository _customersRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IDataAccess dataAccess, IConfiguration config)
        {
            _dataAccess = dataAccess;
            _tokenService = new TokenService(config);
            _customersRepository = new CustomersRepository(dataAccess);
        }

        public ResultData Login(LoginDTO login)
        {
            ResultData result = new();

            try
            {
                var customer = _customersRepository.GetCustomerByLoginAndPassword(login.Login, login.Password).Result;

                if (customer != null)
                {
                    login.AccessToken = _tokenService.BuildToken(customer);
                    result.Data = login;
                    result.Success = true;
                }
            }
            catch(Exception ex)
            {
                result.Error = "Podano błędny adres e-mail lub hasło";
            }
            return result;
        }

        public ResultData Register(RegisterDTO register)
        {
            ResultData result = new();

            try
            {
                var isAlreadyCreated = CheckIfCustomerExistsByLogin(register.Email);

                if (isAlreadyCreated)
                    result.Error = "Użytkownik o podanym adresie e-mail istnieje już w systemie";
                else
                {
                    Customers newCustomer = MapRegisterDataToCustomer(register);
                    _customersRepository.CreateCustomer(newCustomer).GetAwaiter().GetResult();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public Customers MapRegisterDataToCustomer(RegisterDTO register)
        {
            Customers newCustomer = new Customers
            {
                Login = register.Email,
                Password = register.Password,
                FirstName = register.Firstname,
                LastName = register.Lastname,
                AddressLine1 = register.AddressLine1,
                AddressLine2 = register.AddressLine2,
                PostCode = register.PostCode
            };
            return newCustomer;
        }

        public bool CheckIfCustomerExistsByLogin(string login)
        {
            try
            {
                var customer = _customersRepository.GetCustomerByLogin(login).Result;
                return customer != null;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
