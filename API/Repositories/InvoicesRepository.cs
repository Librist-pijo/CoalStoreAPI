using API.Repositories.Interfaces;
using API.Repositories.Models;
using Dapper;
using DataLibrary.DataAccess.Interfaces;
using System.Data;

namespace API.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly IDataAccess _dataAccess;

        public InvoicesRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Create(Invoices invoice)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("OrderId", invoice.OrderId);
            parameters.Add("PaymentMethod", invoice.PaymentMethod);
            parameters.Add("Amount", invoice.Amount);
            parameters.Add("State", invoice.State);
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCreateInvoice", parameters, "SQLDB");
        }

        public Task Delete(int orderId)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("orderId", orderId);

            return _dataAccess.SaveData("dbo.spDeleteInvoice", parameter, "SQLDB");
        }

        public async Task<Invoices> GetById(int invoiceId)
        {
            var invoice = await _dataAccess.LoadData<Invoices, dynamic>
                ("dbo.spGetInvoiceById",
                new
                {
                    Id = invoiceId
                },
                "SQLDB");

            return invoice.FirstOrDefault();
        }

        public async Task<List<Invoices>> Get()
        {
            return await _dataAccess.LoadData<Invoices, dynamic>
             ("dbo.spGetInvoices",
             new
             { },
             "SQLDB");
        }

        public async Task Update(Invoices invoice)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("OrderId", invoice.OrderId);
            parameters.Add("PaymentMethod", invoice.PaymentMethod);
            parameters.Add("Amount", invoice.Amount);
            parameters.Add("State", invoice.State);
            parameters.Add("Id", invoice.Id);

            await _dataAccess.SaveData("dbo.spUpdateInvoice", parameters, "SQLDB");
        }

        public async Task<Invoices> GetByOrderId(int orderId)
        {
            var invoice = await _dataAccess.LoadData<Invoices, dynamic>
                ("dbo.spGetInvoiceByOrderId",
                new
                {
                    orderId = orderId
                },
                "SQLDB");

            return invoice.FirstOrDefault();
        }
    }
}
