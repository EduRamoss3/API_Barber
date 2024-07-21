
namespace Barber.Application.CQRS.Schedule.Commands
{
    public class PatchEndServiceScheduleCommand : ScheduleCommand
    {
        public PatchEndServiceScheduleCommand(int id)
        {
            Id = id;    
        }
    }
}
