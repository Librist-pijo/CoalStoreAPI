namespace API.Services.Interfaces
{
    public interface IOrdersService
    {
        List<EnumDescriptionDTO<OrderState>> GetOrdersStates();
    }
}
