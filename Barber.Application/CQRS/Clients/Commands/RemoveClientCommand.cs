using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Commands
{
    public class RemoveClientCommand : ClientCommand
    {
        public RemoveClientCommand(int id)
        {
            Id = id;
        }   
    }
}
