
using System.ComponentModel.DataAnnotations;

namespace Barber.Application.DTOs.Register
{
    public sealed class BarberRegisterDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required!")]
        [StringLength(200, ErrorMessage = "Max of characters: 200")]
        public string Name { get; set; }

        public BarberRegisterDTO(string name) => Name = name;
       
    }
}
