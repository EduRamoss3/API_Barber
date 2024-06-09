using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Barber.Commands
{
    public class RemoveBarberCommand : BarberCommand
    {
        public RemoveBarberCommand(int id) => Id = id;
    }
}
