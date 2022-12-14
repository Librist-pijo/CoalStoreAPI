using API.ModelsDTO;
using API.ModelsDTO.Products;
using API.Repositories.Factories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using API.Validators.Interfaces;

namespace API.Services
{
    public class ProductsService : IProductsService
    {
        protected readonly IProductsRepository _productsRepository;
        protected readonly IProductsValidator _productsValidator;
        protected readonly ProductsFactory _productsFactory;

        public ProductsService(IProductsRepository productsRepository,
                               IProductsValidator productsValidator)
        {
            _productsRepository = productsRepository;
            _productsValidator = productsValidator;
            _productsFactory = new ProductsFactory();
        }


        public ResultData CreateProducts(CreateProductsDTO ProductsDTO)
        {
            ResultData result = new();

            try
            {
                Products products = _productsFactory.CreateProducts(ProductsDTO);
                var validation = _productsValidator.ValidateCreateAsync(products).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _productsRepository.Create(products);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public ResultData DeleteProducts(DeleteProductsDTO deleteProductsDTO)
        {
            ResultData result = new();

            try
            {
                Products products = _productsFactory.DeleteProducts(deleteProductsDTO);
                var validation = _productsValidator.ValidateDeleteAsync(products).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _productsRepository.Delete(products);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public Products GetProductByName(string productName)
        {
            var category = _productsRepository.GetByName(productName).GetAwaiter().GetResult();
            return category;
        }

        public List<Products> GetProducts()
        {
            var category = _productsRepository.Get().GetAwaiter().GetResult();
            return category;
        }

        public ResultData UpdateProducts(UpdateProductsDTO ProductsDTO)
        {
            ResultData result = new();

            try
            {
                Products products = _productsFactory.UpdateProducts(ProductsDTO);
                var validation = _productsValidator.ValidateUpdateAsync(products).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _productsRepository.Update(products);
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
