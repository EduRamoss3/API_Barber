using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetScheduleByClientIdQuery : IRequest<IEnumerable<Schedules>>
    {
        public int IdClient { get; set; }
        public GetScheduleByClientIdQuery(int idClient) => IdClient = idClient;
        
    }
}
