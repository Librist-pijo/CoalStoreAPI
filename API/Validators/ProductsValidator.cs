using API.ModelsDTO;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;

namespace API.Validators
{
    public class ProductsValidator : IProductsValidator
    {
        private const int MinPrice = 0;
        private const int MinStock = 0;
        private const int MaxStringLength = 255;

        protected readonly IProductsRepository _productsRepository;

        public ProductsValidator(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ResultData> ValidateCreateAsync(Products products)
        {
            var nameExistsValidationTask = await ValidateProductNameExists(products);
            var nameValidationTask = await ValidateName(products);
            var stockValidationTask = await ValidateStock(products);
            var priceValidationTask = await ValidatePrice(products);

            if (!nameExistsValidationTask.Success)
            {
                return nameExistsValidationTask;
            }

            if (!stockValidationTask.Success)
            {
                return stockValidationTask;
            }
            if (!nameValidationTask.Success)
            {
                return nameValidationTask;
            }
            if (!priceValidationTask.Success)
            {
                return nameValidationTask;
            }
            return new ResultData { Success = true };
        }

        public async Task<ResultData> ValidateUpdateAsync(Products products)
        {
            var idExistsValidationTask = await ValidateProductIdExists(products.Id);
            var nameExistsValidationTask = await ValidateProductNameExists(products);
            var stockValidationTask = await ValidateStock(products);
            var nameValidationTask = await ValidateName(products);
            var priceValidationTask = await ValidatePrice(products);

            if (!idExistsValidationTask.Success)
            {
                return idExistsValidationTask;
            }

            if (!nameExistsValidationTask.Success)
            {
                nameExistsValidationTask.Success = true;
                return nameExistsValidationTask;
            }

            if (!stockValidationTask.Success)
            {
                return stockValidationTask;
            }
            if (!nameValidationTask.Success)
            {
                return nameValidationTask;
            }
            if (!priceValidationTask.Success)
            {
                return nameValidationTask;
            }
            return new ResultData { Success = true };
        }

        public async Task<ResultData> ValidateDeleteAsync(int productsId)
        {
            return await ValidateProductIdExists(productsId);
        }

        private async Task<ResultData> ValidateName(Products products)
        {
            var validationResult = new ResultData();
            if (products.Name.Length >= MaxStringLength)
            {
                validationResult.Error = $"Name can have max of: {MaxStringLength} length";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }

        private async Task<ResultData> ValidateProductNameExists(Products products)
        {
            var validationResult = new ResultData();
            var product = await _productsRepository.GetByName(products.Name);

            if (product == null || product == default)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Product with name: {products.Name} already exist in database";
            validationResult.Success = false;
            return validationResult;
        }

        private async Task<ResultData> ValidateProductIdExists(int productsId)
        {
            var validationResult = new ResultData();
            var product = await _productsRepository.GetById(productsId);

            if (product == null || product == default)
            {
                validationResult.Error = $"Product with id: {productsId} do not exist in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }

        private async Task<ResultData> ValidateStock(Products products)
        {
            var validationResult = new ResultData();
            if (products.Stock >= MinStock)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Product stock need to be minimum of: {MinStock}";
            validationResult.Success = false;
            return validationResult;
        }

        private async Task<ResultData> ValidatePrice(Products products)
        {
            var validationResult = new ResultData();
            if (products.Price > MinPrice)
            {
                validationResult.Success = true;
                return validationResult;
            }

            validationResult.Error = $"Product price need to be minimum of: {MinPrice}";
            validationResult.Success = false;
            return validationResult;
        }
    }
}