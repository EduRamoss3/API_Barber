using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Commands
{
    public class UpdateClientCommand : ClientCommand
    {
        public UpdateClientCommand(int id)
        {
            Id = id;
        }   
    }
}
