using Barber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.DTOs
{
    public sealed class BarberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Disponibility { get; set; }
        public List<Schedules> Schedules { get;set; } = new List<Schedules>();
    }
}
