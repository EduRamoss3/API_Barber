using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class PatchEndOpenServiceHandler : IRequestHandler<PatchEndOpenServiceScheduleCommand, bool>
    {
        private readonly IUnityOfWork _uof;
        public PatchEndOpenServiceHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public async Task<bool> Handle(PatchEndOpenServiceScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _uof.SchedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if(schedule is not null)
            {
                await _uof.SchedulesRepository.EndOrOpenServiceByIdAsync(request.Id, request.IsFinalized);
                await _uof.Commit();
                return true;
            }
            return false;
        }
    }
}
