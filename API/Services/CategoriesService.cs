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

        public ResultData CreateCategories(CreateCategoriesDTO categoriesDTO)
        {
            ResultData result = new();

            try
            {
                Categories categories = _categoriesFactory.CreateCategories(categoriesDTO);
                var validation = _categoriesValidator.ValidateCreateAsync(categories).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _categoriesRepository.CreateCategory(categories);
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
                _categoriesRepository.UpdateCategory(categories);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public ResultData DeleteCategories(DeleteCategoriesDTO categoriesDTO)
        {
            ResultData result = new();

            try
            {
                Categories categories = _categoriesFactory.DeleteCategories(categoriesDTO);
                var validation = _categoriesValidator.ValidateDeleteAsync(categories).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _categoriesRepository.DeleteCategory(categories);
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
            var category = _categoriesRepository.GetCategoryByName(categoryName).GetAwaiter().GetResult();
            return category;
        }
        public List<Categories> GetCategories()
        {
            var category = _categoriesRepository.GetCategories().GetAwaiter().GetResult();
            return category;
        }
    }
}
