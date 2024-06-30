using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class RemoveScheduleHandler : IRequestHandler<RemoveScheduleCommand, Schedules>
    {
        private readonly ISchedulesRepository _scheduleRepository;
        public RemoveScheduleHandler(ISchedulesRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Schedules> Handle(RemoveScheduleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedule = await _scheduleRepository.GetScheduleById(request.Id) ?? throw new ApplicationException("Schedule no exist");
            await _scheduleRepository.RemoveSchedule(schedule);
            return schedule;
        }
    }
}
