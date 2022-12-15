using API.ModelsDTO.OrdersProductsDTO;
using API.Repositories.Models;

namespace API.ModelsDTO.Orders
{
    public class CreateOrdersDTO
    {
        public int CustomerId { get; set; }
        public List<CreateOrdersProductsDTO> Products{ get; set;}
        public int Amount { get; set; }
        public string ShippingAddress { get; set; }
        public int State { get; set; }
    }
}
