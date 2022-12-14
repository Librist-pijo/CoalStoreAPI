using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess;
using DataLibrary.DataAccess.Interfaces;
using System.Data;
using System.Runtime.InteropServices;

namespace API.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDataAccess _dataAccess;

        public ProductsRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Create(Products product)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", product.Name);
            parameters.Add("Stock", product.Stock);
            parameters.Add("Price", product.Price);
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCreateProduct", parameters, "SQLDB");
        }

        public Task Delete(Products product)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("Id", product.Id);

            return _dataAccess.SaveData("dbo.spDeleteProduct", parameter, "SQLDB");
        }

        public async Task<Products> GetByName(string productName)
        {
            var product = await _dataAccess.LoadData<Products, dynamic>
                ("dbo.spGetProductByName",
                new
                {
                    Name = productName
                },
                "SQLDB");

            return product.FirstOrDefault();
        }

        public async Task<List<Products>> Get()
        {
           return await _dataAccess.LoadData<Products, dynamic>
                ("dbo.spGetProducts",
                new
                { },
                "SQLDB");
        }

        public Task Update(Products product)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", product.Name);
            parameters.Add("Stock", product.Stock);
            parameters.Add("Price", product.Price);
            parameters.Add("Id", product.Id);

            return _dataAccess.SaveData("dbo.spUpdateProduct", parameters, "SQLDB");
        }

        public async Task<Products> GetById(int productId)
        {
            var product = await _dataAccess.LoadData<Products, dynamic>
                ("dbo.spGetProductById",
                new
                {
                    Id = productId
                },
                "SQLDB");

            return product.FirstOrDefault();
        }
    }
}
