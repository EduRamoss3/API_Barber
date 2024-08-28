using Barber.API.Models;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private readonly UserManager<IdentityUser> _user;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration, IClientService clientService
            , UserManager<IdentityUser> user)
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
        public async Task<ActionResult<TokenViewModel>> Login ([FromBody] LoginModel userInfo)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok("Already authenticated!");
            }
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);
            if (result.IsSucceded)
            {
                var token = await GenerateToken(userInfo);
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

            var expiration = DateTime.UtcNow.AddDays(7); 


            JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims, 
            expires: expiration,
            signingCredentials: credentials
            );

            return new TokenViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Authenticated",
                Authenticated = true,
                Role = role
            };
        }
    }
}
