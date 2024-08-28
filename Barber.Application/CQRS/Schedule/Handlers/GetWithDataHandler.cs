

using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetWithDataHandler : IRequestHandler<GetWithData, List<Schedules>>
    {
        private readonly IUnityOfWork _uof;
        public GetWithDataHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<List<Schedules>> Handle(GetWithData request, CancellationToken cancellationToken)
        {
            var list = await _uof.SchedulesRepository.GetWithData();
            if(list is null)
            {
                return new List<Schedules>();
            }
            return list;
        }
    }
}
