using API.ModelsDTO.InvoicesDTO;
using API.Repositories.Models;

namespace API.Repositories.Factories
{
    public class InvoicesFactory
    {
        public Invoices CreateInvoices(CreateInvoicesDTO invoicesDTO)
        {
            Invoices newInvoice = new Invoices
            {
                OrderId= invoicesDTO.OrderId,
                PaymentMethod= invoicesDTO.PaymentMethod,
                State=invoicesDTO.State,
                Amount=invoicesDTO.Amount 
            };
            return newInvoice;
        }
        public Invoices UpdateInvoices(UpdateInvoicesDTO invoicesDTO)
        {
            Invoices newInvoice = new Invoices
            {
                OrderId = invoicesDTO.OrderId,
                PaymentMethod = invoicesDTO.PaymentMethod,
                State = invoicesDTO.State,
                Amount=invoicesDTO.Amount,
                Id = invoicesDTO.Id
            };
            return newInvoice;
        }
    }
}
