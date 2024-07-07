using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetScheduleByClientIdQueryHandler : IRequestHandler<GetScheduleByClientIdQuery, List<Schedules>>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public GetScheduleByClientIdQueryHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }
        
        public async Task<List<Schedules>> Handle(GetScheduleByClientIdQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _schedulesRepository.GetByClientIdAsync(request.IdClient);
            if(schedules is null)
            {
                throw new ApplicationException("Client dont exist");
            }
           
            return schedules;
        }
    }
}
