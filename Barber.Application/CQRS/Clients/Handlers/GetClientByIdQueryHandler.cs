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
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
    {
        private readonly IClientRepository _clientRepository;
        public GetClientByIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var client = await _clientRepository.GetClientById(request.Id);
            if(client is not Client)
            {
                throw new ApplicationException("Data format error");
            }
            return client;
        }
    }
}
