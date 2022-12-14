using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess.Interfaces;
using System.Data;

namespace API.Repositories
{
    public class OrdersProductsRepository : IOrdersProductsRepository
    {
        private readonly IDataAccess _dataAccess;

        public OrdersProductsRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Creat(OrdersProducts ordersProducts)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("OrderId", ordersProducts.OrderId);
            parameters.Add("ProductId", ordersProducts.ProductId);
            parameters.Add("Quantity", ordersProducts.Quantity);

            await _dataAccess.SaveData("dbo.spCreateOrdersProducts", parameters, "SQLDB");
        }

        public Task Delete(OrdersProducts ordersProducts)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("ProductId", ordersProducts.ProductId);
            parameter.Add("OrderId", ordersProducts.OrderId);

            return _dataAccess.SaveData("dbo.spDeleteOrdersProducts", parameter, "SQLDB");
        }

        public async Task<List<OrdersProducts>> Get()
        {
            return await _dataAccess.LoadData<OrdersProducts, dynamic>
             ("dbo.spGetOrdersProducts",
             new
             { },
             "SQLDB");
        }

        public async Task<List<OrdersProducts>> GetByOrderId(int orderId)
        {
            return await _dataAccess.LoadData<OrdersProducts, dynamic>
             ("dbo.spGetOrdersProductsByOrderId",
             new
             { OrderId = orderId },
             "SQLDB");
        }

        public async Task Update(OrdersProducts ordersProducts)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("OrderId", ordersProducts.OrderId);
            parameters.Add("ProductId", ordersProducts.ProductId);
            parameters.Add("Quantity", ordersProducts.Quantity);

            await _dataAccess.SaveData("dbo.spUpdateOrdersProducts", parameters, "SQLDB");
        }
    }
}
