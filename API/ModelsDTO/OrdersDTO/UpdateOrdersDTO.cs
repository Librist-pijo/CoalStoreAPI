namespace API.ModelsDTO.Orders
{
    public class UpdateOrdersDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string ShippingAddress { get; set; }
        public int State { get; set; }
        public DateTime? ShippingDate { get; set; }
    }
}
