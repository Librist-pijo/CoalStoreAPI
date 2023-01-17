using API.ModelsDTO;
using API.ModelsDTO.CategoriesDTO;
using API.Repositories.Factories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using API.Validators.Interfaces;

namespace API.Services
{
    public class CategoriesService : ICategoriesService
    {
        protected readonly ICategoriesRepository _categoriesRepository;
        protected readonly ICategoriesValidator _categoriesValidator;
        protected readonly CategoriesFactory _categoriesFactory;

        public CategoriesService(ICategoriesRepository categoriesRepository,
                                 ICategoriesValidator categoriesValidator)
        {
            _categoriesRepository = categoriesRepository;
            _categoriesValidator = categoriesValidator;
            _categoriesFactory = new CategoriesFactory();
        }

        public async Task<ResultData> CreateCategories(CreateCategoriesDTO categoriesDTO)
        {
            ResultData result = new();

            try
            {
                Categories categories = _categoriesFactory.CreateCategories(categoriesDTO);
                var validation = await _categoriesValidator.ValidateCreateAsync(categories);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                await _categoriesRepository.Create(categories);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }
        public async Task<ResultData> UpdateCategories(UpdateCategoriesDTO categoriesDTO)
        {
            ResultData result = new();

            try
            {
                Categories categories = _categoriesFactory.UpdateCategories(categoriesDTO);
                var validation = await _categoriesValidator.ValidateUpdateAsync(categories);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                _categoriesRepository.Update(categories);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ResultData> DeleteCategories(int categoryId)
        {
            ResultData result = new();

            try
            {
                var validation = await _categoriesValidator.ValidateDeleteAsync(categoryId);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                _categoriesRepository.Delete(categoryId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }
        public async Task<Categories> GetCategoryByName(string categoryName)
        {
            var category = await _categoriesRepository.GetByName(categoryName);
            return category;
        }
        public async Task<List<Categories>> GetCategories()
        {
            var category = await _categoriesRepository.Get();
            return category;
        }
    }
}
