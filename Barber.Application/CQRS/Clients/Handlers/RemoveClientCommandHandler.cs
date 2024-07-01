using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;
using System.Reflection.Metadata.Ecma335;

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
                throw new ApplicationException("Request dont exist");
            }
            var client = await _clientRepository.GetByIdAsync(request.Id);
            if(client is null)
            {
                throw new ApplicationException("Client dont exist!");
            }
            await _clientRepository.RemoveAsync(client);
            return client;
        }
    }
}
