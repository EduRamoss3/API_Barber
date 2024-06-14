using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Schedule.Commands
{
    public class RemoveScheduleCommand : ScheduleCommand
    {
        public RemoveScheduleCommand(int id)
        {
            Id = id;
        }
    }
}
