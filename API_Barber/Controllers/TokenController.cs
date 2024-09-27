using Barber.API.Models;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Barber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        private readonly IClientService _clientService;
        private readonly UserManager<ApplicationUser> _user;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration, IClientService clientService
            , UserManager<ApplicationUser> user)
        {
            _authenticate = authenticate;
            _configuration = configuration;
            _clientService = clientService;
            _user = user;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] ClientRegisterDTO model)
        {
            var result = await _authenticate.RegisterUser(model.Email, model.Password);
            if (result.IsSucceded)
            {
                await _clientService.AddAsync(model);
                return Ok("Register!");
            }
            foreach(var str in result.Message)
            {
                ModelState.AddModelError($"{str.GetHashCode()}", str);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login ([FromBody] LoginModel userInfo)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok("Already authenticated!");
            }
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);
            if (result.IsSucceded)
            {
                var token = await GenerateToken(userInfo);
                var refreshToken = GenerateRefreshToken();
                _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);
                var isSuccessTokenRefresh = await _authenticate.AddRefreshToken(userInfo.Email, refreshToken, DateTime.Now.AddMinutes(refreshTokenValidityInMinutes));

                token.RefreshToken = refreshToken;
                return Ok(token);
                
            }
            else
            {
                ModelState.AddModelError("Error","Invalid login attempt");
                return BadRequest(ModelState);
            }

        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Authenticate first");
            }
           
            await _authenticate.Logout();
            return Ok();
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config)
        {
            var secretKey = _config["Jwt:SecretKey"] ?? throw new InvalidOperationException("Invalid Key");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }
        private string GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(secureRandomBytes);

            var refreshToken = Convert.ToBase64String(secureRandomBytes);
            return refreshToken;
        }
        private async Task<TokenViewModel> GenerateToken(LoginModel userInfo)
        {
            string role = "Member";
            var user =  await _user.FindByEmailAsync(userInfo.Email);
            if (await _user.IsInRoleAsync(user,"Admin"))
            {
                role = "Admin";
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, role)
            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(_configuration.GetSection("Jwt").GetValue<double>("TokenValidityInMinutes"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                Audience = _configuration.GetSection("Jwt").GetValue<string>("Audience"),
                Issuer = _configuration.GetSection("Jwt").GetValue<string>("Audience"),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return new TokenViewModel()
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration,
                Message = "Authenticated",
                Authenticated = true,
                Role = role,
            };
        }
    }
}
