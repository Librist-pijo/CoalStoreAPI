using API.ModelsDTO;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Validators.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace API.Validators
{
    public class CategoriesValidator : ICategoriesValidator
    {
        protected readonly ICategoriesRepository _categoriesRepository;

        private const int MinCategoryNameLength = 3;
        private const int MaxCategoryNameLength = 255;

        public CategoriesValidator(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ResultData> ValidateCreateAsync(Categories categories)
        {
            var ExistsValidationTask = await ValidateCategoryNameExists(categories);
            var NameValidationTask = await ValidateName(categories);
            if (!NameValidationTask.Success)
            {
                return NameValidationTask;
            }
            if (ExistsValidationTask.Success)
            {
                ExistsValidationTask.Success = false;
                ExistsValidationTask.Error = "Name exists in database";
                return ExistsValidationTask;
            }
            return new ResultData{ Success = true };
        }

        public async Task<ResultData> ValidateDeleteAsync(int categoryId)
        {
            return await ValidateCategoryIdExists(categoryId);
        }

        public async Task<ResultData> ValidateUpdateAsync(Categories categories)
        {
            var idExistsValidationTask = await ValidateCategoryIdExists(categories.Id);
            var nameExistsValidationTask = await ValidateCategoryNameExists(categories);
            var NameValidationTask = await ValidateName(categories);
            if (!NameValidationTask.Success)
            {
                return NameValidationTask;
            }
            if (!idExistsValidationTask.Success)
            {
                return idExistsValidationTask;
            }
            if (nameExistsValidationTask.Success)
            {
                nameExistsValidationTask.Success = false;
                nameExistsValidationTask.Error = "Name exists in database";
                return nameExistsValidationTask;
            }
            return new ResultData { Success = true };
        }

        private async Task<ResultData> ValidateName(Categories categories)
        {
            var validationResult = new ResultData();
            if (categories.Name.Length >= MinCategoryNameLength &&
                categories.Name.Length <= MaxCategoryNameLength)
            {
                validationResult.Success = true;
                return validationResult;
            }
            validationResult.Error = $"Incorrect name, should be between {MinCategoryNameLength} and {MaxCategoryNameLength} characters";
            validationResult.Success = false;
            return validationResult;
        }

        private async Task<ResultData> ValidateCategoryNameExists(Categories categories)
        {
            var validationResult = new ResultData();
            var category = await _categoriesRepository.GetByName(categories.Name);
            if (category == null || category == default)
            {
                validationResult.Error = $"There is no category with name: {categories.Name} in database";
                validationResult.Success = false;
                return validationResult;
            }
            validationResult.Success = true;
            return validationResult;
        }

        private async Task<ResultData> ValidateCategoryIdExists(int categoryId)
        {
            var validationResult = new ResultData();
            var category = await _categoriesRepository.GetById(categoryId);
            if (category == null || category == default)
            {
                validationResult.Error = $"There is no category with id: {categoryId} in database";
                validationResult.Success = false;
                return validationResult;
            }

            validationResult.Success = true;
            return validationResult;
        }
    }
}