using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetSchedulesByBarberIdQueryHandler : IRequestHandler<GetSchedulesByBarberIdQuery, List<Schedules>>
    {
        private readonly ISchedulesRepository _shedulesRepository;
        public GetSchedulesByBarberIdQueryHandler(ISchedulesRepository shedulesRepository)
        {
            _shedulesRepository = shedulesRepository;
        }
        public async Task<List<Schedules>> Handle(GetSchedulesByBarberIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedules = await _shedulesRepository.GetSchedulesByBarberId(request.IdBarber) ?? new List<Schedules>();
            return schedules;
        }
    }
}
