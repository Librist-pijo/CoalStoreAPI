using API.Enums;

namespace API.Repositories.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public DateTimeOffset? ShippingDate { get; set; }
        public int State { get; set; }
        public string OrderNumber
        {
            get { return $"{Id}/{CustomerId}/{OrderDate.DayOfYear}"; }
        }
    }
}
