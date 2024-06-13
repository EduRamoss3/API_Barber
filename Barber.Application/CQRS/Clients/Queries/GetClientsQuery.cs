using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Queries
{
    public class GetClientsQuery : IRequest<IEnumerable<Client>>
    {
    }
}
