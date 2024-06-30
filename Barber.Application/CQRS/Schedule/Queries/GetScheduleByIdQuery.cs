using Barber.Domain.Entities;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetScheduleByIdQuery : IRequest<Schedules>
    {
        public int Id { get; set; }
        public GetScheduleByIdQuery(int id) => Id = id;
    }
}
