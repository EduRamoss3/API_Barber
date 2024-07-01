
using System.ComponentModel.DataAnnotations;
namespace Barber.Application.DTOs.Register
{
    public sealed record ClientRegisterDTO
    {
        [Required(ErrorMessage ="Email is required!")]
        [EmailAddress]
        [StringLength(250, ErrorMessage = "Max 250 characters")]
        public string Email { get; init; }

        [Required(ErrorMessage ="Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Required(ErrorMessage ="Password confirmation is required!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords don't match!")]
        public string ConfirmPassword { get; init; }

        [Required(ErrorMessage ="Name is required!")]
        [StringLength(200, ErrorMessage = "Max 200 characters")]
        public string Name { get; init; }
    }
}
