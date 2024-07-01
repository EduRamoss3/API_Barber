using Barber.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Barber.Application.DTOs
{
    public sealed record BarberDTO
    {
        [Key]
        public int Id { get; init; } 
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(200, ErrorMessage = "Max 200 characters")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Set one disponibility!")]
        public bool Disponibility { get; init; }

        public List<SchedulesDTO> Schedules { get; init; } = new List<SchedulesDTO>();
    }
}
