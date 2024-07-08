using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class PatchEndOpenServiceHandler : IRequestHandler<PatchEndOpenServiceScheduleCommand, bool>
    {
        private readonly ISchedulesRepository _schedulesRepository;

        public PatchEndOpenServiceHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }
        public async Task<bool> Handle(PatchEndOpenServiceScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _schedulesRepository.GetByIdAsync(p => p.Id == request.Id);
            if(schedule is not null)
            {
                await _schedulesRepository.EndOrOpenServiceByIdAsync(request.Id, request.IsFinalized);
                return true;
            }
            return false;
        }
    }
}
