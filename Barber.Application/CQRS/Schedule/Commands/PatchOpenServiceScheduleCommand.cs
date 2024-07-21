namespace Barber.Application.CQRS.Schedule.Commands
{
    public class PatchOpenServiceScheduleCommand : ScheduleCommand
    {
        public PatchOpenServiceScheduleCommand(int id)
        {
            Id = id;
        }
    }
}
