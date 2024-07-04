using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class AddScheduleCommandHandler : IRequestHandler<AddScheduleCommand, Schedules>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public AddScheduleCommandHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }

        public async Task<Schedules> Handle(AddScheduleCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Error, verify all data before register!");
            }
            Schedules schedules = new Schedules(request.IdBarber, request.IdClient, request.TypeOfService, request.DateSchedule, request.ValueForService,request.IsFinalized);
            var listSchedules = await _schedulesRepository.GetSchedulesByBarberId(request.IdBarber);    

            foreach(Schedules schedule in listSchedules)
            {
                if(schedule.DateSchedule == request.DateSchedule && !schedule.IsFinalized)
                {
                    throw new ApplicationException("This time is already scheduled");
                }
            }
            await _schedulesRepository.AddNewSchedule(schedules);
            return schedules;
        }
    }
}
