using Barber.Domain.Entities;
using Barber.Domain.Entities.Enums;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Commands
{
    public class ScheduleCommand : IRequest<Schedules>
    {
        public int Id { get;  set; }
        public int IdBarber { get; private set; }
        public int IdClient { get; private set; }
        public TypeOfService TypeOfService { get; private set; }
        public DateTime DateSchedule { get; private set; }
        public decimal ValueForService { get; set; }
        public bool IsFinalized { get; set; }
    }
}
