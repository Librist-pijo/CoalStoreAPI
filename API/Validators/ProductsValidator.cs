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
            bool NameValidationTask = await ValidateName(products);
            bool StockValidationTask = await ValidateStock(products);
            bool PriceValidationTask = await ValidatePrice(products);

            if (!StockValidationTask)
            {
                return false;
            }
            if (!NameValidationTask)
            {
                return false;
            }
            if (!PriceValidationTask)
            {
                return false;
            }
            return true;
            
        }

        public async Task<bool> ValidateUpdateAsync(Products products)
        {
            bool ExistsValidationTask = await ValidateProductExists(products);
            if (!ExistsValidationTask)
            {
                return false;
            }

            bool StockValidationTask = await ValidateStock(products);
            bool NameValidationTask = await ValidateName(products);
            bool PriceValidationTask = await ValidatePrice(products);

            if (!StockValidationTask)
            {
                return false;
            }
            if (!NameValidationTask)
            {
                return false;
            }
            if (!PriceValidationTask)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(Products products)
        {
            bool ExistsValidationTask = await ValidateProductExists(products);
            if (!ExistsValidationTask)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateName(Products products)
        {
            var product = await _productsRepository.GetProductByName(products.Name);

            if (product == null || product == default)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ValidateProductExists(Products products)
        {
            var product = await _productsRepository.GetProductByName(products.Name);

            if (product == null || product == default)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateStock(Products products)
        {
            var product = await _productsRepository.GetProductByName(products.Name);

            if (product.Stock > 0)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ValidatePrice(Products products)
        {
            var product = await _productsRepository.GetProductByName(products.Name);

            if (product.Price > 0)
            {
                return true;
            }

            return false;
        }


    }
}