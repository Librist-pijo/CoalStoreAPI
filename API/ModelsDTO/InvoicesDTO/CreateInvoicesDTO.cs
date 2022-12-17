namespace API.ModelsDTO.InvoicesDTO
{
    public class CreateInvoicesDTO
    {
        public int OrderId { get; set; }
        public int PaymentMethod { get; set; }
        public int State { get; set; }
        public decimal Amount { get; set; }
    }
}
