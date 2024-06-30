using Barber.Domain.Entities;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetSchedulesByBarberIdQuery : IRequest<IEnumerable<Schedules>>
    {
        public int IdBarber { get; set; }
        public GetSchedulesByBarberIdQuery(int idBarber)
        {
            IdBarber = idBarber;
        }   
    }
}
