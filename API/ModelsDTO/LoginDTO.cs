using API.Enums;

namespace API.ModelsDTO
{
    public class LoginDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public GrantType GrantType { get; set; }
    }
}
