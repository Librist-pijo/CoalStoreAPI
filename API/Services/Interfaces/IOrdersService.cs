using API.Enums;
using API.ModelsDTO;

namespace API.Services.Interfaces
{
    public interface IOrdersService
    {
        List<EnumDescriptionDTO<OrderState>> GetOrdersStates();
    }
}
