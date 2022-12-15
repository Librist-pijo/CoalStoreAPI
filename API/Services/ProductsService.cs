using API.ModelsDTO;
using API.ModelsDTO.ProductsDTO;
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
        protected readonly IProductsCategoriesService _productsCategoriesService;
        protected readonly ICategoriesService _categoriesService;

        public ProductsService(IProductsRepository productsRepository,
                               IProductsValidator productsValidator,
                               IProductsCategoriesService productsCategoriesService,
                               ICategoriesService categoriesService)
        {
            _productsRepository = productsRepository;
            _productsValidator = productsValidator;
            _productsFactory = new ProductsFactory();
            _productsCategoriesService = productsCategoriesService;
            _categoriesService = categoriesService;
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

        public ResultData CreateProductsCategoriesMapping(ProductsCategories productsCategories)
        {
            ResultData result = new();

            try
            {
                result = _productsCategoriesService.CreateProductsCategories(productsCategories);  
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public ResultData DeleteProducts(int productId)
        {
            ResultData result = new();

            try
            {
                var validation = _productsValidator.ValidateDeleteAsync(productId).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _productsRepository.Delete(productId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public ResultData DeleteProductsCategoriesMapping(int Id)
        {
            ResultData result = new();

            try
            {
                result = _productsCategoriesService.DeleteProductsCategories(Id);
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

        public List<ProductsWithCategory> GetProductsWithCategory()
        {
            List<ProductsWithCategory> productsWithCategories = new List<ProductsWithCategory>();
            var categories = _categoriesService.GetCategories();
            var products = GetProducts();
            foreach (var product in products)
            {
                var productsCategories = _productsCategoriesService.GetProductsCategoriesByProductId(product.Id);
                foreach (var category in productsCategories)
                {
                    productsWithCategories.Add(MapProductsToCategory(product.Name,
                                                                     categories.Where(x => x.Id == category.CategoryId).FirstOrDefault().Name,
                                                                     category.Id));
                }
            }
            return productsWithCategories;
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

        public ResultData UpdateProductsCategoriesMapping(ProductsCategories productsCategories)
        {
            ResultData result = new();

            try
            {
                result = _productsCategoriesService.UpdateProductsCategories(productsCategories);
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        private ProductsWithCategory MapProductsToCategory(string productName, string categoryName, int Id)
        {
            var productsWithCategory = new ProductsWithCategory
            {
                ProductName= productName,
                CategoryName= categoryName,
                Id= Id
            };
            return productsWithCategory;
        }
    }
}
