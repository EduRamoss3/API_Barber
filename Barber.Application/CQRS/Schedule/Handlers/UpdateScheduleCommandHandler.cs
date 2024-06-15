using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateValueForScheduleCommand, Schedules>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public UpdateScheduleCommandHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }
        public async Task<Schedules> Handle(UpdateValueForScheduleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var schedule = await _schedulesRepository.GetScheduleById(request.Id) ?? throw new ApplicationException("Schedule no exist");
            schedule.UpdateValueForService(request.ValueForService);
            await _schedulesRepository.UpdateValueForSchedule(schedule);
            return schedule;
        }
    }
}
