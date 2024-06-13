using Barber.Application.CQRS.Clients.Commands;
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
    public class RemoveClientCommandHandler : IRequestHandler<RemoveClientCommand, Client>
    {
        private readonly IClientRepository _clientRepository;
        public RemoveClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Client dont exist");
            }
            var client = await _clientRepository.GetClientById(request.Id);
            await _clientRepository.RemoveClient(client);
            return client;
        }
    }
}
