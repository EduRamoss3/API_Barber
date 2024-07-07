using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class RemoveScheduleHandler : IRequestHandler<RemoveScheduleCommand, bool>
    {
        private readonly ISchedulesRepository _scheduleRepository;
        public RemoveScheduleHandler(ISchedulesRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<bool> Handle(RemoveScheduleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedule = await _scheduleRepository.GetByIdAsync(request.Id);
            if(schedule is null)
            {
                return false;
            }
            await _scheduleRepository.RemoveAsync(schedule);
            return true;
        }
    }
}
