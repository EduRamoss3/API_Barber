using MediatR;

namespace Barber.Application.CQRS.Barber.Queries
{
    public class GetIndisponibleDatesQuery : IRequest<List<DateTime>>
    {
        public int IdBarber { get; set; }
        public GetIndisponibleDatesQuery(int idBarber) => IdBarber = idBarber;
    }
}
