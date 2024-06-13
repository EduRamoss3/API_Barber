using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Queries
{
    public class GetClientByIdQuery : IRequest<Client>
    {
        public int Id { get; set; } 
        public GetClientByIdQuery(int id)
        {
            Id = id;
        }
    }
}
