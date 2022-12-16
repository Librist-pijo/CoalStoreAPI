namespace API.ModelsDTO.Orders
{
    public class UpdateOrdersDTO
    {
        public int Id { get; set; }
        public string ShippingAddress { get; set; }
        public int State { get; set; }
        public DateTime? ShippingDate { get; set; }
    }
}
