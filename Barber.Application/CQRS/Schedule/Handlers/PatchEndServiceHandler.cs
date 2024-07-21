using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class PatchEndServiceHandler : IRequestHandler<PatchEndServiceScheduleCommand, Schedules>
    {
        private readonly IUnityOfWork _uof;

        public PatchEndServiceHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public async Task<Schedules> Handle(PatchEndServiceScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _uof.SchedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if (schedule is not null)
            {
                await _uof.SchedulesRepository.EndOrOpenServiceByIdAsync(request.Id, false);
                await _uof.Commit();
                return schedule;
            }
            return null;
        }
    }
}
