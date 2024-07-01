using Barber.Domain.Entities;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetSchedulesQuery : IRequest<List<Schedules>>
    {
    }
}
