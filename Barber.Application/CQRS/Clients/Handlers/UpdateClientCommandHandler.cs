using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                return false;
            }
            var client = await _clientRepository.GetByIdAsync(p => p.Id == request.Id);
            if(client is null)
            {
                return false;
            }
            client.Update(request.Name, request.Points, request.Scheduled, request.LastTimeHere);
            await _clientRepository.UpdateAsync(client);
            return true;
            
        }
    }
}
