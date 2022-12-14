using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess.Interfaces;
using System.Data;

namespace API.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDataAccess _dataAccess;

        public CategoriesRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task CreateCategory(Categories category)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", category.Name);
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCreateCategory", parameters, "SQLDB");
        }

        public Task DeleteCategory(Categories category)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("Id", category.Id);

            return _dataAccess.SaveData("dbo.spDeleteCategory", parameter, "SQLDB");
        }

        public async Task<List<Categories>> GetCategories()
        {
            return await _dataAccess.LoadData<Categories, dynamic>
                 ("dbo.spGetCategories",
                 new
                 { },
                 "SQLDB");
        }

        public async Task<Categories> GetCategoryById(int categoryId)
        {
            var category = await _dataAccess.LoadData<Categories, dynamic>
                ("dbo.spGetCategoryById",
                new
                {
                    Id = categoryId
                },
                "SQLDB");

            return category.FirstOrDefault();
        }

        public async Task<Categories> GetCategoryByName(string categoryName)
        {
            var category = await _dataAccess.LoadData<Categories, dynamic>
                ("dbo.spGetCategoryByName",
                new
                {
                    Name = categoryName
                },
                "SQLDB");

            return category.FirstOrDefault();
        }

        public async Task UpdateCategory(Categories category)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", category.Name);
            parameters.Add("Id", category.Id);

            await _dataAccess.SaveData("dbo.spUpdateCategory", parameters, "SQLDB");
        }
    }
}