using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetScheduleByClientIdQueryHandler : IRequestHandler<GetScheduleByClientIdQuery, IEnumerable<Schedules>>
    {
        private readonly ISchedulesRepository _schedulesRepository;
        public GetScheduleByClientIdQueryHandler(ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository;
        }
        
        public async Task<IEnumerable<Schedules>> Handle(GetScheduleByClientIdQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _schedulesRepository.GetScheduleByClientId(request.IdClient);
            if(request is null)
            {
                throw new ApplicationException("Client dont exist");
            }
            if(schedules.Count() == 0)
            {
                return Enumerable.Empty<Schedules>();
            }
            return schedules;
        }
    }
}
