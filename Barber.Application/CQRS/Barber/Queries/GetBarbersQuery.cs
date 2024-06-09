using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Barber.Queries
{
    public class GetBarbersQuery : IRequest<IEnumerable<BarberMain>>
    {
    }
}
