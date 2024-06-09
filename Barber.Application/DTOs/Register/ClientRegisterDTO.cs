
using System.ComponentModel.DataAnnotations;
namespace Barber.Application.DTOs.Register
{
    public sealed class ClientRegisterDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Max of characters: 200")]
        public string Name { get; set; }
    }
}
