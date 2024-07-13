using Barber.Domain.Entities;
using Barber.Domain.Parameters;
using MediatR;
namespace Barber.Application.CQRS.Barber.Queries
{
    public class GetBarbersQuery : IRequest<IEnumerable<BarberMain>>
    {
        public GetParametersPagination ParametersPagination { get; set; }
        public GetBarbersQuery(GetParametersPagination parameters)
        {
            ParametersPagination = parameters;
        }
    }
}
