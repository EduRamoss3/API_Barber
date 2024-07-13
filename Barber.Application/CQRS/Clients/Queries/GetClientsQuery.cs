using Barber.Domain.Entities;
using Barber.Domain.Parameters;
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
        public ParametersToPagination ParametersPagination { get; set; }
        public GetClientsQuery(ParametersToPagination parametersPagination)
        {
            ParametersPagination = parametersPagination;
        }
    }
}
