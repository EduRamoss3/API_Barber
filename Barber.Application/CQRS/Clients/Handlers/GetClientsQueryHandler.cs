using Barber.Application.CQRS.Clients.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<Client>>
    {
        private readonly IClientRepository _clientRepository;
        public GetClientsQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }   

        public async Task<IEnumerable<Client>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            return await _clientRepository.GetClients();
        }
    }
}
