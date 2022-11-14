namespace API.Repositories.Models
{
    public class OrdersProducts
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
