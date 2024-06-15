using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Schedule.Queries
{
    public class GetSchedulesByBarberIdQuery : IRequest<IEnumerable<Schedules>>
    {
        public int IdBarber { get; set; }
        public GetSchedulesByBarberIdQuery(int idBarber)
        {
            IdBarber = idBarber;
        }   
    }
}
