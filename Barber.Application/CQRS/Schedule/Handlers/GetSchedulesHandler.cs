using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetSchedulesHandler : IRequestHandler<GetSchedulesQuery, List<Schedules>>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public GetSchedulesHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }
        public async Task<List<Schedules>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            return await _schedulesRepository.GetAllAsync() ?? new List<Schedules>();
        }
    }
}
