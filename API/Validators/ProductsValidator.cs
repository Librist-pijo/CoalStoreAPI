using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;

namespace API.Validators
{
    public class ProductsValidator : IProductsValidator
    {
        protected readonly IProductsRepository _productsRepository;

        public ProductsValidator(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<bool> ValidateCreateAsync(Products products)
        {
            bool nameExistsValidationTask = await ValidateProductNameExists(products);
            bool nameValidationTask = await ValidateName(products);
            bool stockValidationTask = await ValidateStock(products);
            bool priceValidationTask = await ValidatePrice(products);

            if (!stockValidationTask
                || !nameValidationTask
                || !priceValidationTask
                || nameExistsValidationTask)
            {
                return false;
            }
            return true;
            
        }

        public async Task<bool> ValidateUpdateAsync(Products products)
        {
            bool idExistsValidationTask = await ValidateProductIdExists(products.Id);
            bool nameExistsValidationTask = await ValidateProductNameExists(products);
            bool stockValidationTask = await ValidateStock(products);
            bool NameValidationTask = await ValidateName(products);
            bool priceValidationTask = await ValidatePrice(products);

            if (!stockValidationTask
                || !NameValidationTask
                || !priceValidationTask
                || !idExistsValidationTask
                || nameExistsValidationTask)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(int productsId)
        {
            bool idExistsValidationTask = await ValidateProductIdExists(productsId);
            if (!idExistsValidationTask)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateName(Products products)
        {
            if (products.Name.Length > 255)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateProductNameExists(Products products)
        {
            var product = await _productsRepository.GetByName(products.Name);

            if (product == null || product == default)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateProductIdExists(int productsId)
        {
            var product = await _productsRepository.GetById(productsId);

            if (product == null || product == default)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateStock(Products products)
        {
            if (products.Stock >= 0)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ValidatePrice(Products products)
        {
            if (products.Price > 0)
            {
                return true;
            }

            return false;
        }


    }
}