using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Barber.Commands
{
    public class RemoveBarberCommand : IRequest<bool>
    {
        public int Id {  get; set; }
        public RemoveBarberCommand(int id) => Id = id;
    }
}
