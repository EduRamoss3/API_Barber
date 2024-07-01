using Barber.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Barber.Application.DTOs
{
    public sealed class BarberDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required!")]
        [StringLength(200, ErrorMessage = "Max 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Set one disponibility!")]
        public bool Disponibility { get; set; }

        public List<Schedules> Schedules { get;set; } = new List<Schedules>();
    }
}
