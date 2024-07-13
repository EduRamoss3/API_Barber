using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetSchedulesHandler : IRequestHandler<GetSchedulesQuery,IEnumerable<Schedules>>
    {
        private readonly IUnityOfWork _uof; 
        public GetSchedulesHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public async Task<IEnumerable<Schedules>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            return await _uof.SchedulesRepository.GetAllAsync(request.ParametersPagination) ?? new List<Schedules>();
        }
    }
}
