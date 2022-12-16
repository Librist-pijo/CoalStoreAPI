using API.ModelsDTO.Orders;
using API.ModelsDTO;
using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface IInvoicesService
    {
        public ResultData CreateInvoice(Invoices invoices);
        public ResultData UpdateInvoice(Invoices invoices);
        public ResultData DeleteInvoice(int orderId);
    }
}
