
using System.ComponentModel.DataAnnotations;
namespace Barber.Application.DTOs.Register
{
    public sealed class ClientRegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords don't match!")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}
