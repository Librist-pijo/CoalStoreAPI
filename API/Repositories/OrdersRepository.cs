using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess.Interfaces;
using System.Data;

namespace API.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDataAccess _dataAccess;

        public OrdersRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> Create(Orders order)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("CustomerId", order.CustomerId);
            parameters.Add("OrderDate", order.OrderDate);
            parameters.Add("ShippingAddress", order.ShippingAddress);
            parameters.Add("ShippingDate", order.ShippingDate);
            parameters.Add("State", order.State);
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCreateOrder", parameters, "SQLDB");
            return parameters.Get<int>("Id");
        }

        public Task Delete(int orderId)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("Id", orderId);

            return _dataAccess.SaveData("dbo.spDeleteOrder", parameter, "SQLDB");
        }

        public async Task<Orders> GetById(int orderId)
        {
            var order = await _dataAccess.LoadData<Orders, dynamic>
                ("dbo.spGetOrderById",
                new
                {
                    Id = orderId
                },
                "SQLDB");

            return order.FirstOrDefault();
        }

        public async Task<List<Orders>> Get()
        {
            return await _dataAccess.LoadData<Orders, dynamic>
             ("dbo.spGetOrders",
             new
             { },
             "SQLDB");
        }

        public async Task Update(Orders order)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("CustomerId", order.CustomerId);
            parameters.Add("OrderDate", order.OrderDate);
            parameters.Add("ShippingAddress", order.ShippingAddress);
            parameters.Add("ShippingDate", order.ShippingDate);
            parameters.Add("State", order.State);
            parameters.Add("Id", order.Id);

            await _dataAccess.SaveData("dbo.spUpdateOrder", parameters, "SQLDB");
        }
    }
}
