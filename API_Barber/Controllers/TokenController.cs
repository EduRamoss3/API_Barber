﻿using Barber.API.Models;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public TokenController(IAuthenticate authenticate, IConfiguration configuration, IClientService clientService)
        {
            _authenticate = authenticate;
            _configuration = configuration;
            _clientService = clientService;
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
        public async Task<ActionResult<UserToken>> Login ([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);
            if (result.IsSucceded)
            {
                return GenerateToken(userInfo);
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
       
        private UserToken GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims, 
            expires: expiration,
            signingCredentials: credentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
