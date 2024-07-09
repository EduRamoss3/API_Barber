
using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetDisponibleScheduleByDateQueryHandler : IRequestHandler<GetDisponibleScheduleByDateQuery, bool>
    {
        private readonly IUnityOfWork _uof; 
        public GetDisponibleScheduleByDateQueryHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(GetDisponibleScheduleByDateQuery request, CancellationToken cancellationToken)
        {
             if(request is null)
             {
                throw new ApplicationException("Error on API request");
             }
            return await _uof.SchedulesRepository.IsDisponibleInThisDate(request.IdBarber, request.DateTimeSearch);
        }
    }
}
