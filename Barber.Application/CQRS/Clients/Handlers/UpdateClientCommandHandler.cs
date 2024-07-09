using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, bool>
    {
        private readonly IUnityOfWork _uof; 
        public UpdateClientCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                return false;
            }
            var client = await _uof.ClientRepository.GetByIdAsync(p => p.Id == request.Id);
            if(client is null)
            {
                return false;
            }
            client.Update(request.Name, request.Points, request.Scheduled, request.LastTimeHere);
            _uof.ClientRepository.Update(client);
            await _uof.Commit();
            return true;
            
        }
    }
}
