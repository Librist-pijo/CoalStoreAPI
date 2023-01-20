using API.ModelsDTO;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace API.Validators
{
    public class CustomersValidator : ICustomersValidator
    {

        const int MinStringLength = 3;
        const int MaxStringLength = 255;
        const int MinPasswordLength = 10;
        protected readonly ICustomersRepository _customersRepository;
        public CustomersValidator(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<ResultData> ValidateCreateAsync(Customers customers)
        {
            var CustomerExistsValidation = await ValidateCustomerExists(customers);
            if (CustomerExistsValidation.Success)
            {
                CustomerExistsValidation.Success = false;
                CustomerExistsValidation.Error = "Customer exists in database";
                return CustomerExistsValidation;
            }
            var LoginValidation = await ValidateLogin(customers);
            var PasswordValidation = await ValidatePassword(customers);
            var FirstNameValidation = await ValidateFirstName(customers);
            var LastNameValidation = await ValidateLastName(customers);
            var AddressLine1Validation = await ValidateAddressLine1(customers);
            var AddressLine2Validation = await ValidateAddressLine2(customers);
            var PostCodeValidation = await ValidatePostCode(customers);
            if (!LoginValidation.Success)
            {
                return LoginValidation;
            }
            if (!PasswordValidation.Success)
            {
                return PasswordValidation;
            }
            if (!FirstNameValidation.Success)
            {
                return FirstNameValidation;
            }

            if (!LastNameValidation.Success)
            {
                return LastNameValidation;
            }

            if (!AddressLine1Validation.Success)
            {
                return AddressLine1Validation;
            }

            if (!AddressLine2Validation.Success)
            {
                return AddressLine2Validation;
            }
            if (!PostCodeValidation.Success)
            {
                return PostCodeValidation;
            }
            return new ResultData { Success = true };
        }

        public async Task<ResultData> ValidateDeleteAsync(Customers customers)
        {
            return await ValidateCustomerExists(customers);
        }

        public async Task<ResultData> ValidateUpdateAsync(Customers customers)
        {
            var CustomerExistsValidation = await ValidateCustomerExists(customers);
            if (!CustomerExistsValidation.Success)
            {
                return CustomerExistsValidation;
            }
            var LoginValidation = await ValidateLogin(customers);
            var PasswordValidation = await ValidatePassword(customers);
            var FirstNameValidation = await ValidateFirstName(customers);
            var LastNameValidation = await ValidateLastName(customers);
            var AddressLine1Validation = await ValidateAddressLine1(customers);
            var AddressLine2Validation = await ValidateAddressLine2(customers);
            var PostCodeValidation = await ValidatePostCode(customers);
            if (!LoginValidation.Success)
            {
                return LoginValidation;
            }
            if(!PasswordValidation.Success)
            {
                return PasswordValidation;
            }
            if (!FirstNameValidation.Success)
            {
                return FirstNameValidation;
            }

            if (!LastNameValidation.Success)
            {
                return LastNameValidation;
            }

            if (!AddressLine1Validation.Success)
            {
                return AddressLine1Validation;
            }

            if (!AddressLine2Validation.Success)
            {
                return AddressLine2Validation;
            }
            if (!PostCodeValidation.Success)
            {
                return PostCodeValidation;
            }
            return new ResultData { Success = true };
        }

        private async Task<ResultData> ValidateLogin(Customers customers)
        {
            var validationResult = new ResultData();
            try
            {
                MailAddress m = new MailAddress(customers.Login);
                validationResult.Success = true;
                return validationResult;
            }
            catch (FormatException)
            {
                validationResult.Error = $"Wrong email format: {customers.Login}";
                validationResult.Success = false;
                return validationResult;
            }
        }
        private async Task<ResultData> ValidatePassword(Customers customers) 
        {
            var validationResult = new ResultData();
            if (customers.Password.Length > MinPasswordLength)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Password not matching minimum length: {MinPasswordLength}";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateFirstName(Customers customers) 
        {
            var validationResult = new ResultData();
            if (customers.FirstName == null)
            {
                validationResult.Success = true;
                return validationResult;
            }
            if(customers.FirstName.Length > MinStringLength &&
               customers.FirstName.Length <= MaxStringLength)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Incorrect {customers.FirstName}, should be between {MinStringLength} and {MaxStringLength} characters";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateLastName(Customers customers)
        {
            var validationResult = new ResultData();
            if (customers.LastName == null)
            {
                validationResult.Success = true;
                return validationResult;
            }
            if (customers.LastName.Length > MinStringLength &&
                customers.LastName.Length <= MaxStringLength)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Incorrect {customers.LastName}, should be between {MinStringLength} and {MaxStringLength} characters";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateAddressLine1(Customers customers)
        {
            var validationResult = new ResultData();
            if (customers.AddressLine1 == null)
            {
                validationResult.Success = true;
                return validationResult;
            }
            if (customers.AddressLine1.Length > MinStringLength &&
                customers.AddressLine1.Length <= MaxStringLength)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Incorrect {customers.AddressLine1}, should be between {MinStringLength} and {MaxStringLength} characters";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateAddressLine2(Customers customers)
        {
            var validationResult = new ResultData();
            if (customers.AddressLine2 == null)
            {
                validationResult.Success = true;
                return validationResult;
            }
            if (customers.AddressLine2.Length > MinStringLength &&
                customers.AddressLine2.Length <= MaxStringLength)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Incorrect {customers.AddressLine2}, should be between {MinStringLength} and {MaxStringLength} characters";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidatePostCode(Customers customers)
        {
            var validationResult = new ResultData();
            string pattern = @"^[0-9]{2}-[0-9]{3}";
            Regex regex = new Regex(pattern);
            if (customers.PostCode == null)
            {
                validationResult.Success = true;
                return validationResult;
            }
            if (regex.IsMatch(customers.PostCode))
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Incorrect {customers.PostCode}, should be in 00-000 format";
            validationResult.Success = false;
            return validationResult;
        }
        private async Task<ResultData> ValidateCustomerExists(Customers customers)
        {
            var validationResult = new ResultData();
            var customer = await _customersRepository.GetCustomerByLogin(customers.Login);

            if (customer == null || customer == default)
            {
                validationResult.Error = $"Customer with login: {customers.Login} do not exists";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }
    }
}
