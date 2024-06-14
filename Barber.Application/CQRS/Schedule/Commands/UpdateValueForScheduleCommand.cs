using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Schedule.Commands
{
    public class UpdateValueForScheduleCommand : ScheduleCommand
    {
        public UpdateValueForScheduleCommand(int id) => Id = id;
    }
}
