using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace API.Validators
{
    public class CustomersValidator : ICustomersValidator
    {
        protected readonly ICustomersRepository _customersRepository;
        public CustomersValidator(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<bool> ValidateCreateAsync(Customers customers)
        {
            bool CustomerExistsValidation = await ValidateCustomerExists(customers);
            if (!CustomerExistsValidation)
            {
                return false;
            }
            bool LoginValidation = await ValidateLogin(customers);
            bool PasswordValidation = await ValidatePassword(customers);
            bool FirstNameValidation = await ValidateFirstName(customers);
            bool LastNameValidation = await ValidateLastName(customers);
            bool AddressLine1Validation = await ValidateAddressLine1(customers);
            bool AddressLine2Validation = await ValidateAddressLine2(customers);
            bool PostCodeValidation = await ValidatePostCode(customers);
            if (!LoginValidation
                || !PasswordValidation
                || !FirstNameValidation
                || !LastNameValidation
                || !AddressLine1Validation
                || !AddressLine2Validation
                || !PostCodeValidation)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(Customers customers)
        {
            bool CustomerExistsValidation = await ValidateCustomerExists(customers);
            if (!CustomerExistsValidation)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUpdateAsync(Customers customers)
        {
            bool CustomerExistsValidation = await ValidateCustomerExists(customers);
            if (!CustomerExistsValidation)
            {
                return false;
            }
            bool LoginValidation = await ValidateLogin(customers);
            bool PasswordValidation = await ValidatePassword(customers);
            bool FirstNameValidation = await ValidateFirstName(customers);
            bool LastNameValidation = await ValidateLastName(customers);
            bool AddressLine1Validation = await ValidateAddressLine1(customers);
            bool AddressLine2Validation = await ValidateAddressLine2(customers);
            bool PostCodeValidation = await ValidatePostCode(customers);
            if (!LoginValidation
                || !PasswordValidation
                || !FirstNameValidation
                || !LastNameValidation
                || !AddressLine1Validation
                || !AddressLine2Validation
                || !PostCodeValidation)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateLogin(Customers customers)
        {
            try
            {
                MailAddress m = new MailAddress(customers.Login);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private async Task<bool> ValidatePassword(Customers customers) 
        {
            if (customers.Password.Length > 10)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateFirstName(Customers customers) 
        {
            if (customers.FirstName == null)
            {
                return true;
            }
            if(customers.FirstName.Length > 3)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateLastName(Customers customers)
        {
            if (customers.LastName == null)
            {
                return true;
            }
            if (customers.LastName.Length > 3)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateAddressLine1(Customers customers)
        {
            if (customers.AddressLine1 == null)
            {
                return true;
            }
            if (customers.AddressLine1.Length > 3)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateAddressLine2(Customers customers)
        {
            if (customers.AddressLine2 == null)
            {
                return true;
            }
            if (customers.AddressLine2.Length > 3)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidatePostCode(Customers customers)
        {
            string pattern = @"^[0-9]{2}-[0-9]{3}";
            Regex regex = new Regex(pattern);
            if (customers.PostCode == null)
            {
                return true;
            }
            if (regex.IsMatch(customers.PostCode))
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ValidateCustomerExists(Customers customers)
        {
            var customer = await _customersRepository.GetCustomerByLogin(customers.Login);

            if (customer == null || customer == default)
            {
                return false;
            }

            return true;
        }
    }
}
