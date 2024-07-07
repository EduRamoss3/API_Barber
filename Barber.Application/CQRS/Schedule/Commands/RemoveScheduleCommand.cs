
using MediatR;

namespace Barber.Application.CQRS.Schedule.Commands
{
    public class RemoveScheduleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public RemoveScheduleCommand(int id)
        {
            Id = id;
        }
    }
}
