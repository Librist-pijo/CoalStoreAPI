using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;

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

        public async Task<bool> ValidateCreateAsync(ProductsCategories productsCategories)
        {
            bool pairExists = await ValidateProductsCategoriesExists(productsCategories);
            bool productExists = await ValidateProductsExists(productsCategories);
            bool categoryExists = await ValidateCategoriesExists(productsCategories);
            if (pairExists
                || !productExists
                || !categoryExists)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(int Id)
        {
            bool exists = await ValidateProductsCategoriesIdExists(Id);
            if (exists)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUpdateAsync(ProductsCategories productsCategories)
        {
            
            bool productExists = await ValidateProductsExists(productsCategories);
            bool categoryExists = await ValidateCategoriesExists(productsCategories);
            bool productWithCategoryExists = await ValidateProductWithCategoryExists(productsCategories);
            if (!productExists
                || !categoryExists
                || !productWithCategoryExists)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateProductsExists(ProductsCategories productsCategories)
        {
            var product = await _productsRepository.GetById(productsCategories.ProductId);

            if (product == null || product == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateCategoriesExists(ProductsCategories productsCategories)
        {
            var category = await _categoriesRepository.GetById(productsCategories.CategoryId);

            if (category == null || category == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateProductsCategoriesExists(ProductsCategories productsCategories)
        {
            var pair = await _productsCategoriesRepository.GetPair(productsCategories.ProductId,
                                                  productsCategories.CategoryId);
            if (pair == null || pair == default)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateProductsCategoriesIdExists(int Id)
        {
            var exist = await _productsCategoriesRepository.GetById(Id);
            if (exist == null || exist == default)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateProductWithCategoryExists(ProductsCategories productsCategories)
        {
            var product = await _productsCategoriesRepository.GetByByProductId(productsCategories.ProductId);
            if (product == null || !product.Any())
            {
                return false;
            }
            if (product.Contains(productsCategories))
            {
                return false;
            }
            return true;
        }

    }
}
