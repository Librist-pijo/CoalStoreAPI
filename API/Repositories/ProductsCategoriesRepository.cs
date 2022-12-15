using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess.Interfaces;
using System.Data;
using System.Reflection.Metadata;

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
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCreateProductsCategories", parameters, "SQLDB");
        }

        public Task Delete(int Id)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("Id", Id);

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
             ("dbo.spGetProductsCategoriesByCategoryId",
             new
             { CategoryId = categoryId },
             "SQLDB");
        }

        public async Task<List<ProductsCategories>> GetByByProductId(int productId)
        {
            return await _dataAccess.LoadData<ProductsCategories, dynamic>
             ("dbo.spGetProductsCategoriesByProductId",
             new
             { ProductId = productId },
             "SQLDB");
        }

        public async Task<ProductsCategories> GetPair(int productId, int categoryId)
        {
            var product = await _dataAccess.LoadData<ProductsCategories, dynamic>
                ("dbo.spGetProductsCategoriesPair",
                new
                {
                    ProductId = productId,
                    CategoryId = categoryId
                },
                "SQLDB");

            return product.FirstOrDefault();
        }
        public async Task<ProductsCategories> GetById(int Id)
        {
            var product = await _dataAccess.LoadData<ProductsCategories, dynamic>
                ("dbo.spGetProductsCategoriesById",
                new
                {
                    Id = Id
                },
                "SQLDB");

            return product.FirstOrDefault();
        }

        public async Task Update(ProductsCategories productCategories)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("CategoryId", productCategories.CategoryId);
            parameters.Add("ProductId", productCategories.ProductId);
            parameters.Add("Id", productCategories.Id);

            await _dataAccess.SaveData("dbo.spUpdateProductsCategories", parameters, "SQLDB");
        }
    }
}
