using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Commands
{
    public class ClientCommand : IRequest<Client>
    {
        public int Id { get; set; }
        public bool Scheduled { get; set; }
        public DateTime LastTimeHere { get; set; }
        public Schedules Schedule { get; set; }
    }
}
