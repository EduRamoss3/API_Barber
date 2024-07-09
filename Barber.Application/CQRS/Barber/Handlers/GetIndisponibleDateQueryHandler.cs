
using Barber.Application.CQRS.Barber.Queries;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Barber.Handlers
{
    public class GetIndisponibleDateQueryHandler : IRequestHandler<GetIndisponibleDatesQuery, List<DateTime>>
    {
        private readonly IUnityOfWork _uof;
        public GetIndisponibleDateQueryHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<List<DateTime>> Handle(GetIndisponibleDatesQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Error in request");
            }
            var listSchedulesIndisponible = await _uof.SchedulesRepository.GetIndisponibleDatesByBarberId(request.IdBarber);
            return listSchedulesIndisponible;
        }
    }
}
