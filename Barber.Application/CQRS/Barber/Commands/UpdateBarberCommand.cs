using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Barber.Commands
{
    public class UpdateBarberCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public bool Disponibility { get; set; }
        public UpdateBarberCommand(int id, bool disponibility)
        {
            Id = id;
            Disponibility = disponibility;
        } 
    }
}
