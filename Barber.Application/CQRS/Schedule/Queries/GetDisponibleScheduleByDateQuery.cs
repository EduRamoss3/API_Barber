using MediatR;

namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetDisponibleScheduleByDateQuery : IRequest<bool>
    {
        public int IdBarber { get; set; }
        public DateTime DateTimeSearch { get; set; }    
        public GetDisponibleScheduleByDateQuery(int idBarber, DateTime dateTime)
        {
            IdBarber = idBarber;    
            DateTimeSearch = dateTime;  
        }
    }
}
