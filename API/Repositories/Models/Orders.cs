using API.Enums;

namespace API.Repositories.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime? ShippingDate { get; set; }
        public OrderState State { get; set; }
    }
}
