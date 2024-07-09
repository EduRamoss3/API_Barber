using Barber.Application.CQRS.Schedule.Commands;
using Barber.Application.DefaultValues;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Domain.Validation;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class AddScheduleCommandHandler : IRequestHandler<AddScheduleCommand, Schedules>
    {
        private readonly IUnityOfWork _uof; 
        public AddScheduleCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        private bool IsValidScheduleTime(DateTime dateSchedule)
        {
            return dateSchedule.Minute % HourServiceTimeDefault.DefaultMinutes == 0;
        }

        public async Task<Schedules> Handle(AddScheduleCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Error, verify all data before register!");
            }
            var listSchedules = await _uof.SchedulesRepository.GetByBarberIdAsync(request.IdBarber);
            var dateNow = DateTime.Now;

            foreach (Schedules schedule in listSchedules)
            {
                if (request.DateSchedule == schedule.DateSchedule && !schedule.IsFinalized)
                {
                    throw new DomainExceptionValidation("This time is already scheduled");
                }
                if (!IsValidScheduleTime(request.DateSchedule))
                {
                    throw new DomainExceptionValidation($"Only every {HourServiceTimeDefault.DefaultMinutes} minutes");
                }
                if (dateNow > request.DateSchedule)
                {
                    throw new DomainExceptionValidation("Cannot schedule date pass");
                }
            }
            Schedules schedules = new Schedules(request.IdBarber, request.IdClient, 
                request.TypeOfService, request.DateSchedule, request.ValueForService,request.IsFinalized);

           
            await _uof.SchedulesRepository.AddAsync(schedules);
            await _uof.Commit();
            return schedules;
        }
    }
}
