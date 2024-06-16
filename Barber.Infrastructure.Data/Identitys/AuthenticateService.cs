using Barber.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Infrastructure.Data.Identitys
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;    
        public AuthenticateService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
    
        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var identityUser = new IdentityUser()
            {
                UserName = email,
                Email = email
            };
            var result = await _userManager.CreateAsync(identityUser, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(identityUser, isPersistent: false);
                return true;
            }
            return result.Succeeded;
        }
    }
}
