
using System.ComponentModel.DataAnnotations;

namespace Barber.Application.DTOs.Register
{
    public sealed record BarberRegisterDTO
    {
        [Required(ErrorMessage ="Name is required!")]
        [StringLength(200, ErrorMessage = "Max of characters: 200")]
        public string Name { get; init; }

        public BarberRegisterDTO(string name) => Name = name;
       
    }
}
