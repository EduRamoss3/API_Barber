using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Domain.Validation;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetScheduleByClientIdQueryHandler : IRequestHandler<GetScheduleByClientIdQuery, List<Schedules>>
    {
        private readonly IUnityOfWork _uof;
        public GetScheduleByClientIdQueryHandler(IUnityOfWork uof)
        {
            _uof = uof; 
        }
        
        public async Task<List<Schedules>> Handle(GetScheduleByClientIdQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _uof.SchedulesRepository.GetByClientIdAsync(request.IdClient);
            if(schedules.Count == 0)
            {
                return null;
            }
            return schedules;
        }
    }
}
