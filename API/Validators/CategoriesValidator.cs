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
            bool ExistsValidationTask = await ValidateCategoryNameExists(categories);
            bool NameValidationTask = await ValidateName(categories);
            if (!NameValidationTask
                || ExistsValidationTask)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateDeleteAsync(Categories categories)
        {
            bool ExistsValidationTask = await ValidateCategoryIdExists(categories);
            if (!ExistsValidationTask)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateUpdateAsync(Categories categories)
        {
            bool idExistsValidationTask = await ValidateCategoryIdExists(categories);
            bool nameExistsValidationTask = await ValidateCategoryNameExists(categories);
            bool NameValidationTask = await ValidateName(categories);
            if (!NameValidationTask
                || !idExistsValidationTask
                || nameExistsValidationTask)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateName(Categories categories)
        {
            if (categories.Name.Length > 3)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> ValidateCategoryNameExists(Categories categories)
        {
            var category = await _categoriesRepository.GetCategoryByName(categories.Name);
            if (category == null || category == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateCategoryIdExists(Categories categories)
        {
            var category = await _categoriesRepository.GetCategoryById(categories.Id);
            if (category == null || category == default)
            {
                return false;
            }

            return true;
        }
    }
}
