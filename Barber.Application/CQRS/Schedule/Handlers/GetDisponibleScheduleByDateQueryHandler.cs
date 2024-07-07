
using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetDisponibleScheduleByDateQueryHandler : IRequestHandler<GetDisponibleScheduleByDateQuery, bool>
    {
        private readonly ISchedulesRepository _scheduleRepository;
        public GetDisponibleScheduleByDateQueryHandler(ISchedulesRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<bool> Handle(GetDisponibleScheduleByDateQuery request, CancellationToken cancellationToken)
        {
             if(request is null)
             {
                throw new ApplicationException("Error on API request");
             }
            return await _scheduleRepository.IsDisponibleInThisDate(request.IdBarber, request.DateTimeSearch);
        }
    }
}
