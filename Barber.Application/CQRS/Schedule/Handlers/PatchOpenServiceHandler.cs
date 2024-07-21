using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class PatchOpenServiceHandler : IRequestHandler<PatchOpenServiceScheduleCommand, Schedules>
    {
        private readonly IUnityOfWork _uof;
        public PatchOpenServiceHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public async Task<Schedules> Handle(PatchOpenServiceScheduleCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var schedule = await _uof.SchedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if(schedule is null)
            {
                return null;
            }

            await _uof.SchedulesRepository.EndOrOpenServiceByIdAsync(request.Id, true);
            await _uof.ClientRepository.UpdatePointsAsync(request.IdClient);
            await _uof.Commit();
            return schedule;
          
        }
    }
}
