using Barber.Domain.Entities;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetScheduleByClientIdQuery : IRequest<List<Schedules>>
    {
        public int IdClient { get; set; }
        public GetScheduleByClientIdQuery(int idClient) => IdClient = idClient;
        
    }
}
