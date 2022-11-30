using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;

namespace API.Validators
{
    public class CategoriesValidator : ICategoriesValidator
    {
        protected readonly ICategoriesRepository _categoriesRepository;

        public CategoriesValidator(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<bool> ValidateCreateAsync(Categories categories)
        {
            bool ExistsValidationTask = await ValidateCategoryExists(categories);
            if (!ExistsValidationTask)
            {
                return false;
            }
            bool NameValidationTask = await ValidateName(categories);
            if (!NameValidationTask)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(Categories categories)
        {
            bool ExistsValidationTask = await ValidateCategoryExists(categories);
            if (!ExistsValidationTask)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateUpdateAsync(Categories categories)
        {
            bool ExistsValidationTask = await ValidateCategoryExists(categories);
            if (!ExistsValidationTask)
            {
                return false;
            }
            bool NameValidationTask = await ValidateName(categories);
            if (!NameValidationTask)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateName(Categories categories)
        {
            var category = await _categoriesRepository.GetCategoryByName(categories.Name);

            if (category == null || category == default)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ValidateCategoryExists(Categories categories)
        {
            var category = await _categoriesRepository.GetCategoryByName(categories.Name);

            if (category == null || category == default)
            {
                return false;
            }

            return true;
        }
    }
}
