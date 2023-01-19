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


        public async Task<ResultData> CreateProducts(CreateProductsDTO ProductsDTO)
        {
            ResultData result = new();

            try
            {
                Products products = _productsFactory.CreateProducts(ProductsDTO);
                var validation = await _productsValidator.ValidateCreateAsync(products);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _productsRepository.Create(products);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ResultData> CreateProductsCategoriesMapping(ProductsCategories productsCategories)
        {
            ResultData result = new();

            try
            {
                result = await _productsCategoriesService.CreateProductsCategories(productsCategories);  
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ResultData> DeleteProducts(int productId)
        {
            ResultData result = new();

            try
            {
                var validation = await _productsValidator.ValidateDeleteAsync(productId);
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                await _productsRepository.Delete(productId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public async Task<ResultData> DeleteProductsCategoriesMapping(int Id)
        {
            ResultData result = new();

            try
            {
                result = await _productsCategoriesService.DeleteProductsCategories(Id);
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<Products> GetProductByName(string productName)
        {
            var product = await _productsRepository.GetByName(productName);
            return product;
        }

        public async Task<Products> GetProductById(int productId)
        {
            var product = await _productsRepository.GetById(productId);
            return product;
        }

        public async Task<List<Products>> GetProducts()
        {
            var category = await _productsRepository.Get();
            return category;
        }

        public async Task<List<ProductsWithCategory>> GetProductsWithCategory()
        {
            List<ProductsWithCategory> productsWithCategories = new List<ProductsWithCategory>();
            var categories = await _categoriesService.GetCategories();
            var products = await GetProducts();
            foreach (var product in products)
            {
                var productsCategories = await _productsCategoriesService.GetProductsCategoriesByProductId(product.Id);
                foreach (var category in productsCategories)
                {
                    productsWithCategories.Add(MapProductsToCategory(product.Name,
                                                                     categories.Where(x => x.Id == category.CategoryId).FirstOrDefault().Name,
                                                                     category.Id));
                }
            }
            return productsWithCategories;
        }

        public async Task<ResultData> UpdateProducts(UpdateProductsDTO ProductsDTO)
        {
            ResultData result = new();

            try
            {
                Products products = _productsFactory.UpdateProducts(ProductsDTO);
                var validation = await _productsValidator.ValidateUpdateAsync(products);
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

        public async Task<ResultData> UpdateProductsCategoriesMapping(ProductsCategories productsCategories)
        {
            ResultData result = new();

            try
            {
                result = await _productsCategoriesService.UpdateProductsCategories(productsCategories);
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
