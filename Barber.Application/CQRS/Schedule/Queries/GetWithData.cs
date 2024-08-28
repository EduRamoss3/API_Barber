
using Barber.Domain.Entities;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetWithData : IRequest<List<Schedules>>
    {
    }
}
