using Barber.Domain.Interfaces;
using Barber.Domain.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace Barber.Infrastructure.Data.Identitys
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AuthenticateService> _logger;

        public AuthenticateService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AuthenticateService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<Validator> Authenticate(string email, string password)
        {
            _logger.LogInformation($"Attempting to authenticate user with email '{email}'.");

            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

            Validator validate = new Validator()
            {
                IsSucceded = result.Succeeded,
            };

            if (result.Succeeded)
            {
                _logger.LogInformation($"User '{email}' authenticated successfully.");
            }
            else
            {
                _logger.LogWarning($"Failed authentication attempt for user '{email}'.");
            }

            return validate;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"User logged out.");
        }

        public async Task<Validator> RegisterUser(string email, string password)
        {
            _logger.LogInformation($"Attempting to register user with email '{email}'.");

            var identityUser = new IdentityUser()
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(identityUser, password);

            Validator validate = new Validator()
            {
                IsSucceded = result.Succeeded,
            };

            if (result.Succeeded)
            {
                _logger.LogInformation($"User '{email}' registered successfully.");
                await _userManager.AddToRoleAsync(identityUser, "Member");
                await _signInManager.SignInAsync(identityUser, isPersistent: false);
            }
            else
            {
                _logger.LogWarning($"Failed registration attempt for user '{email}'.");
                foreach (var error in result.Errors)
                {
                    validate.Message.Add(error.Description);
                }
            }

            return validate;
        }
    }
}
