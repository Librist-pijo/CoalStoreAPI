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

        public ResultData CreateProductsCategories(ProductsCategories productCategories)
        {
            ResultData result = new();

            try
            {
                var validation = _productsCategoriesValidator.ValidateCreateAsync(productCategories).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _productsCategoriesRepository.Create(productCategories);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public ResultData DeleteProductsCategories(int Id)
        {
            ResultData result = new();

            try
            {
                var validation = _productsCategoriesValidator.ValidateDeleteAsync(Id).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _productsCategoriesRepository.Delete(Id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public List<ProductsCategories> GetProductsCategoriesByProductId(int productId)
        {
            var category = _productsCategoriesRepository.GetByByProductId(productId).GetAwaiter().GetResult();
            return category;
        }

        public ResultData UpdateProductsCategories(ProductsCategories productCategories)
        {
            ResultData result = new();

            try
            {
                var validation = _productsCategoriesValidator.ValidateUpdateAsync(productCategories).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _productsCategoriesRepository.Update(productCategories);
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
