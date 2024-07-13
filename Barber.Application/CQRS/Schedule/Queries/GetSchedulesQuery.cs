using Barber.Domain.Entities;
using Barber.Domain.Parameters;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetSchedulesQuery : IRequest<IEnumerable<Schedules>>
    {
        public GetParametersPagination ParametersPagination { get; set; }
        public GetSchedulesQuery(GetParametersPagination parametersPagination)
        {
            ParametersPagination = parametersPagination;
        }
    }
}
