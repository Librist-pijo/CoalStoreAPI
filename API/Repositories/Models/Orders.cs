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
        public int State { get; set; }
        //
        public string OrderNumber
        {
            get { return $"{Id}/{CustomerId}/{OrderDate.Date}"; }
        }
    }
}
