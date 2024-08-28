using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Queries
{
    public class GetClienteIdByEmailQuery : IRequest<int>
    {
        public string Email { get; set; }   
        public GetClienteIdByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
