using API.Services.Interfaces;

namespace API.Services
{
    public class OrdersService : IOrdersService
    {
        public OrdersService()
        {

        }



        public List<EnumDescriptionDTO<OrderState>> GetOrdersStates()
        {
            try
            {
                List<EnumDescriptionDTO<OrderState>> result = EnumService<OrderState>.GetEnumValues();
                return result;
            }
            catch (Exception ex)
            {
                return new List<EnumDescriptionDTO<OrderState>>();
            }
        }
    }
}
