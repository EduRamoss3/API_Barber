using Barber.Domain.Entities;
using Barber.Domain.Parameters;
using MediatR;
namespace Barber.Application.CQRS.Barber.Queries
{
    public class GetBarbersQuery : IRequest<IEnumerable<BarberMain>>
    {
        public ParametersToPagination ParametersPagination { get; set; }
        public GetBarbersQuery(ParametersToPagination parameters)
        {
            ParametersPagination = parameters;
        }
    }
}
