using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class RemoveScheduleHandler : IRequestHandler<RemoveScheduleCommand, bool>
    {
        private readonly IUnityOfWork _uof; 
        public RemoveScheduleHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(RemoveScheduleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedule = await _uof.SchedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if(schedule is null)
            {
                return false;
            }
            await _uof.SchedulesRepository.RemoveAsync(schedule);
            await _uof.Commit();
            return true;
        }
    }
}
