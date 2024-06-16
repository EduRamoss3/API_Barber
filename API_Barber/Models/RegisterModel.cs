using System.ComponentModel.DataAnnotations;

namespace Barber.API.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password",ErrorMessage = "Passwords don't match!")]
        public string ConfirmPassword { get; set;}
    }
}
