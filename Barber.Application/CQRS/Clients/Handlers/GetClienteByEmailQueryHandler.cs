using Barber.Application.CQRS.Clients.Queries;
using Barber.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class GetClienteByEmailQueryHandler : IRequestHandler<GetClienteIdByEmailQuery, int>
    {
        private readonly IUnityOfWork _uof;
        public GetClienteByEmailQueryHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public Task<int> Handle(GetClienteIdByEmailQuery request, CancellationToken cancellationToken)
        {
            return _uof.ClientRepository.GetIdByEmailAsync(request.Email);
        }
    }
}
