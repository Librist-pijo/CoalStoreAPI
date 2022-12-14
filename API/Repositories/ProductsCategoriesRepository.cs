using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess.Interfaces;
using System.Data;

namespace API.Repositories
{
    public class ProductsCategoriesRepository : IProductsCategoriesRepository
    {
        private readonly IDataAccess _dataAccess;

        public ProductsCategoriesRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Create(ProductsCategories productCategories)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("CategoryId", productCategories.CategoryId);
            parameters.Add("ProductId", productCategories.ProductId);

            await _dataAccess.SaveData("dbo.spCreateProductsCategories", parameters, "SQLDB");
        }

        public Task Delete(ProductsCategories productCategories)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("ProductId", productCategories.ProductId);
            parameter.Add("CategoryId", productCategories.CategoryId);

            return _dataAccess.SaveData("dbo.spDeleteProductsCategories", parameter, "SQLDB");
        }

        public async Task<List<ProductsCategories>> Get()
        {
            return await _dataAccess.LoadData<ProductsCategories, dynamic>
             ("dbo.spGetProductsCategories",
             new
             { },
             "SQLDB");
        }

        public async Task<List<ProductsCategories>> GetByByCategoryId(int categoryId)
        {
            return await _dataAccess.LoadData<ProductsCategories, dynamic>
             ("dbo.spGetProductsCategories",
             new
             { CategoryId = categoryId },
             "SQLDB");
        }

        public async Task<List<ProductsCategories>> GetByByProductId(int productId)
        {
            return await _dataAccess.LoadData<ProductsCategories, dynamic>
             ("dbo.spGetProductsCategories",
             new
             { ProductId = productId },
             "SQLDB");
        }

        public async Task Update(ProductsCategories productCategories)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("CategoryId", productCategories.CategoryId);
            parameters.Add("ProductId", productCategories.ProductId);

            await _dataAccess.SaveData("dbo.spUpdateProductsCategories", parameters, "SQLDB");
        }
    }
}
