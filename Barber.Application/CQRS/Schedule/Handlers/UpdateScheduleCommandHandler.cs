using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, bool>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public UpdateScheduleCommandHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }

        public async Task<bool> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Cant update this schedule!");
            }
            var schedule = await _schedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if(schedule is null)
            {
                return false;
            }
            schedule.Update(request.IdBarber,request.IdClient,request.TypeOfService, request.DateSchedule, request.ValueForService, request.IsFinalized);
            await _schedulesRepository.UpdateAsync(schedule);
            return true;
        }
    }
}
