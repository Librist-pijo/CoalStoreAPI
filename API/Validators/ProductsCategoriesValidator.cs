using API.ModelsDTO;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace API.Validators
{
    public class ProductsCategoriesValidator : IProductsCategoriesValidator
    {

        protected readonly IProductsCategoriesRepository _productsCategoriesRepository;
        protected readonly ICategoriesRepository _categoriesRepository;
        protected readonly IProductsRepository _productsRepository;

        public ProductsCategoriesValidator(IProductsCategoriesRepository productsCategoriesRepository,
                                           ICategoriesRepository categoriesRepository,
                                           IProductsRepository productsRepository)
        {
            _productsCategoriesRepository = productsCategoriesRepository;
            _categoriesRepository = categoriesRepository;
            _productsRepository = productsRepository;
        }

        public async Task<ResultData> ValidateCreateAsync(ProductsCategories productsCategories)
        {
            var pairExists = await ValidateProductsCategoriesExists(productsCategories);
            var productExists = await ValidateProductsExists(productsCategories);
            var categoryExists = await ValidateCategoriesExists(productsCategories);
            if (pairExists.Success)
            {
                pairExists.Success = false;
                pairExists.Error = "Pair already exists in database";
                return pairExists;
            }
            if (!productExists.Success)
            {
                return productExists;
            }
            if (!categoryExists.Success)
            {
                return categoryExists;
            }
            return new ResultData { Success = true };
        }

        public async Task<ResultData> ValidateDeleteAsync(int Id)
        {
            return await ValidateProductsCategoriesIdExists(Id);
        }

        public async Task<ResultData> ValidateUpdateAsync(ProductsCategories productsCategories)
        {
            
            var productExists = await ValidateProductsExists(productsCategories);
            var categoryExists = await ValidateCategoriesExists(productsCategories);
            var productWithCategoryExists = await ValidateProductWithCategoryExists(productsCategories);
            if (!productWithCategoryExists.Success)
            {
                return productWithCategoryExists;
            }
            if (!productExists.Success)
            {
                return productExists;
            }
            if (!categoryExists.Success)
            {
                return categoryExists;
            }
            return new ResultData { Success = true };
        }

        private async Task<ResultData> ValidateProductsExists(ProductsCategories productsCategories)
        {
            var validationResult = new ResultData();
            var product = await _productsRepository.GetById(productsCategories.ProductId);

            if (product == null || product == default)
            {
                validationResult.Error = $"Product with id: {productsCategories.ProductId} do not exist in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }
        private async Task<ResultData> ValidateCategoriesExists(ProductsCategories productsCategories)
        {
            var validationResult = new ResultData();
            var category = await _categoriesRepository.GetById(productsCategories.CategoryId);

            if (category == null || category == default)
            {
                validationResult.Error = $"Category with id: {productsCategories.CategoryId} do not exist in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }
        private async Task<ResultData> ValidateProductsCategoriesExists(ProductsCategories productsCategories)
        {
            var validationResult = new ResultData();
            var pair = await _productsCategoriesRepository.GetPair(productsCategories.ProductId,
                                                  productsCategories.CategoryId);
            if (pair == null || pair == default)
            {
                validationResult.Error = $"Pair with categoryid: {productsCategories.CategoryId} and productid: {productsCategories.ProductId} do not exist in database";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }

        private async Task<ResultData> ValidateProductsCategoriesIdExists(int Id)
        {
            var validationResult = new ResultData();
            var exist = await _productsCategoriesRepository.GetById(Id);
            if (exist == null || exist == default)
            {
                validationResult.Error = $"ProductCategory with id: {Id} do not exists in database";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }

        private async Task<ResultData> ValidateProductWithCategoryExists(ProductsCategories productsCategories)
        {
            var validationResult = new ResultData();
            var product = await _productsCategoriesRepository.GetByByProductId(productsCategories.ProductId);
            if (product == null || !product.Any())
            {
                validationResult.Error = $"Product with id: {productsCategories.ProductId} do not exists";
                validationResult.Success = false;
                return validationResult;
            }
            if (product.Contains(productsCategories))
            {
                validationResult.Error = $"Product already have this category mapped: {productsCategories.CategoryId}";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }

    }
}
