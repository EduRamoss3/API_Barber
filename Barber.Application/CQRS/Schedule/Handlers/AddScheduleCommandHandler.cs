using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Domain.Validation;
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
            var listSchedules = await _schedulesRepository.GetSchedulesByBarberId(request.IdBarber);

            foreach (Schedules schedule in listSchedules)
            {
                if (request.DateSchedule == schedule.DateSchedule && !request.IsFinalized)
                {
                    throw new DomainExceptionValidation("This time is already scheduled");
                }
                if (!(request.DateSchedule.Minute.Equals(30) || request.DateSchedule.Minute.Equals(00)))
                {
                    throw new DomainExceptionValidation("Only every 30 minutes");
                }
            }
            Schedules schedules = new Schedules(request.IdBarber, request.IdClient, 
                request.TypeOfService, request.DateSchedule, request.ValueForService,request.IsFinalized);

           
            await _schedulesRepository.AddNewSchedule(schedules);
            return schedules;
        }
    }
}
