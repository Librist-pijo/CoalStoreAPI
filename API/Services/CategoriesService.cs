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
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
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
        public ResultData UpdateCategories(UpdateCategoriesDTO categoriesDTO)
        {
            ResultData result = new();

            try
            {
                Categories categories = _categoriesFactory.UpdateCategories(categoriesDTO);
                var validation = _categoriesValidator.ValidateUpdateAsync(categories).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
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

        public ResultData DeleteCategories(int categoryId)
        {
            ResultData result = new();

            try
            {
                var validation = _categoriesValidator.ValidateDeleteAsync(categoryId).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
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
        public Categories GetCategoryByName(string categoryName)
        {
            var category = _categoriesRepository.GetByName(categoryName).GetAwaiter().GetResult();
            return category;
        }
        public List<Categories> GetCategories()
        {
            var category = _categoriesRepository.Get().GetAwaiter().GetResult();
            return category;
        }
    }
}
