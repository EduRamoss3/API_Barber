using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Domain.Validation;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetSchedulesByBarberIdQueryHandler : IRequestHandler<GetSchedulesByBarberIdQuery, List<Schedules>>
    {
        private readonly IUnityOfWork _uof;
        public GetSchedulesByBarberIdQueryHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public async Task<List<Schedules>> Handle(GetSchedulesByBarberIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedules = await _uof.SchedulesRepository.GetByBarberIdAsync(request.IdBarber);
            return schedules;
        }
    }
}
