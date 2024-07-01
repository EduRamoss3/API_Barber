using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Schedules>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public UpdateScheduleCommandHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }

        public async Task<Schedules> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Cant update this schedule!");
            }
            var schedule = await _schedulesRepository.GetScheduleById(request.Id) 
                ?? throw new ApplicationException("Schedule no exist!");
            schedule.Update(request.IdBarber,request.IdClient,request.TypeOfService, request.DateSchedule, request.ValueForService, request.IsFinalized);
            await _schedulesRepository.UpdateSchedule(schedule);
            return schedule;
        }
    }
}
