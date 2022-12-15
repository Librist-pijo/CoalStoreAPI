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

        public async Task<bool> ValidateDeleteAsync(int categoryId)
        {
            bool ExistsValidationTask = await ValidateCategoryIdExists(categoryId);
            if (!ExistsValidationTask)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateUpdateAsync(Categories categories)
        {
            bool idExistsValidationTask = await ValidateCategoryIdExists(categories.Id);
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
            var category = await _categoriesRepository.GetByName(categories.Name);
            if (category == null || category == default)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateCategoryIdExists(int categoryId)
        {
            var category = await _categoriesRepository.GetById(categoryId);
            if (category == null || category == default)
            {
                return false;
            }

            return true;
        }
    }
}
