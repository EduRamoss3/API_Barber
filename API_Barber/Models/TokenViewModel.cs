using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace Barber.API.Models
{
    public class TokenViewModel
    {
        public bool Authenticated { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }
    }
}
