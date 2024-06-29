

namespace Barber.Application.CQRS.Schedule.Commands
{
    public class UpdateValueForScheduleCommand : ScheduleCommand
    {
        public UpdateValueForScheduleCommand(int id, decimal valueForService)
        {
            Id = id;
            ValueForService = valueForService;
        }
    }
}
