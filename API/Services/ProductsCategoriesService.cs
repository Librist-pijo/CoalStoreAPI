using API.ModelsDTO;
using API.Repositories.Factories;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using API.Validators;
using API.Validators.Interfaces;

namespace API.Services
{
    public class ProductsCategoriesService : IProductsCategoriesService
    {
        protected readonly IProductsCategoriesRepository _productsCategoriesRepository;
        protected readonly IProductsCategoriesValidator _productsCategoriesValidator;

        public ProductsCategoriesService(IProductsCategoriesRepository productsCategoriesRepository,
                                         IProductsCategoriesValidator productsCategoriesValidator)
        {
            _productsCategoriesRepository = productsCategoriesRepository;
            _productsCategoriesValidator = productsCategoriesValidator;
        }

        public async Task<ResultData> CreateProductsCategories(ProductsCategories productCategories)
        {
            ResultData result = new();

            try
            {
                var validation = await _productsCategoriesValidator.ValidateCreateAsync(productCategories);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _productsCategoriesRepository.Create(productCategories);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ResultData> DeleteProductsCategories(int Id)
        {
            ResultData result = new();

            try
            {
                var validation = await _productsCategoriesValidator.ValidateDeleteAsync(Id);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _productsCategoriesRepository.Delete(Id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public async Task<List<ProductsCategories>> GetProductsCategoriesByProductId(int productId)
        {
            var category = await _productsCategoriesRepository.GetByByProductId(productId);
            return category;
        }

        public async Task<ResultData> UpdateProductsCategories(ProductsCategories productCategories)
        {
            ResultData result = new();

            try
            {
                var validation = await _productsCategoriesValidator.ValidateUpdateAsync(productCategories);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _productsCategoriesRepository.Update(productCategories);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }
    }
}
