using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, Schedules>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public GetScheduleByIdQueryHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository; 
        }
        public async Task<Schedules> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedule = await _schedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if(schedule is null)
            {
                throw new ApplicationException("Schedule not found");
            }
            return schedule;
           
        }
    }
}
