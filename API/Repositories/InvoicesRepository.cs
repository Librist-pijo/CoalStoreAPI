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

        public async Task CreateInvoice(Invoices invoice)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("OrderId", invoice.OrderId);
            parameters.Add("PaymentMethodId", invoice.PaymentMethodId);
            parameters.Add("Amount", invoice.Amount);
            parameters.Add("State", invoice.State);
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCreateInvoice", parameters, "SQLDB");
        }

        public Task DeleteInvoice(Invoices invoice)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("Id", invoice.Id);

            return _dataAccess.SaveData("dbo.spDeleteInvoice", parameter, "SQLDB");
        }

        public async Task<Invoices> GetInvoiceById(int invoiceId)
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

        public async Task<List<Invoices>> GetInvoices()
        {
            return await _dataAccess.LoadData<Invoices, dynamic>
             ("dbo.spGetInvoices",
             new
             { },
             "SQLDB");
        }

        public async Task UpdateInvoice(Invoices invoice)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("OrderId", invoice.OrderId);
            parameters.Add("PaymentMethodId", invoice.PaymentMethodId);
            parameters.Add("Amount", invoice.Amount);
            parameters.Add("State", invoice.State);
            parameters.Add("Id", invoice.Id);

            await _dataAccess.SaveData("dbo.spUpdateInvoice", parameters, "SQLDB");
        }
    }
}
