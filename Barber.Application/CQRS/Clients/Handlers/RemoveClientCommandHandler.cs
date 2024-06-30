using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

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
            var client = await _clientRepository.GetByIdAsync(request.Id);
            await _clientRepository.RemoveAsync(client);
            return client;
        }
    }
}
