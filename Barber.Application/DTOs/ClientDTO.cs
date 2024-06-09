using Barber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.DTOs
{
    public sealed class ClientDTO
    {
        public int Id { get; set; }
        public bool Scheduled { get; set; }
        public DateTime LastTimeHere { get; set; }
        public Schedules Schedule { get; set; }
    }
}
