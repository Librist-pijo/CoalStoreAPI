using API.Enums;
using API.ModelsDTO;

namespace API.Services.Interfaces
{
    public interface IAuthService
    {
        ResultData Login(LoginDTO login);
        ResultData Register(RegisterDTO register);
        bool CheckIfCustomerExistsByLogin(string login);
        bool CheckIfTokenIsValid(string token);
    }
}
