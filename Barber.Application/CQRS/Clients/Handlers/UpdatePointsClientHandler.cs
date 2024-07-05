

using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class UpdatePointsClientHandler : IRequestHandler<UpdatePointsClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        public UpdatePointsClientHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<bool> Handle(UpdatePointsClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id);
            if(client is not null)
            {
                await _clientRepository.UpdatePointsAsync(client.Id);
                return true;
            }
            return false;
        }
    }
}
