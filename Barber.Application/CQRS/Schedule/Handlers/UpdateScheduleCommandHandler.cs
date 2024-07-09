using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, bool>
    {
        private readonly IUnityOfWork _uof; 
        public UpdateScheduleCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Cant update this schedule!");
            }
            var schedule = await _uof.SchedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if(schedule is null)
            {
                return false;
            }
            schedule.Update(request.IdBarber,request.IdClient,request.TypeOfService, request.DateSchedule, request.ValueForService, request.IsFinalized);
            await _uof.SchedulesRepository.UpdateAsync(schedule);
            await _uof.Commit();
            return true;
        }
    }
}
