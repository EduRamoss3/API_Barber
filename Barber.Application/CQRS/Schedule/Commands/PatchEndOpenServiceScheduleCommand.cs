

using MediatR;

namespace Barber.Application.CQRS.Schedule.Commands
{
    public class PatchEndOpenServiceScheduleCommand : IRequest<bool>
    {
        public int Id { get; set; } 
        public bool IsFinalized { get; set; }
        public PatchEndOpenServiceScheduleCommand(int id, bool endOpen)
        {
            Id = id;    
            IsFinalized = endOpen;
        }
    }
}
