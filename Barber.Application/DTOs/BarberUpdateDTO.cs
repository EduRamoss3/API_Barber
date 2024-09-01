using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.DTOs
{
    public sealed record BarberUpdateDTO
    {
        [Key]
        public int Id { get; init; }
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(200, ErrorMessage = "Max 200 characters")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Set one disponibility!")]
        public bool Disponibility { get; init; }
    }
}
