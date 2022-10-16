using API.Enums;

namespace API.Repositories.Models
{
    public class Invoices
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public InvoiceState State { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}
