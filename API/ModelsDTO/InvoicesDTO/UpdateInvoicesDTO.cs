namespace API.ModelsDTO.InvoicesDTO
{
    public class UpdateInvoicesDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PaymentMethod { get; set; }
        public int State { get; set; }
        public decimal Amount { get; set; }
    }
}
