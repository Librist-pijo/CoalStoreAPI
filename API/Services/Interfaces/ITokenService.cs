using API.Repositories.Models;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(Customers user);
        bool IsTokenValid(string token);
    }

}
