

using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class UpdatePointsClientHandler : IRequestHandler<UpdatePointsClientCommand, bool>
    {
        private readonly IUnityOfWork _uof; 
        public UpdatePointsClientHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public async Task<bool> Handle(UpdatePointsClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _uof.ClientRepository.GetByIdAsync(p => p.Id == request.Id);
            if(client is not null)
            {
                await _uof.ClientRepository.UpdatePointsAsync(client.Id);
                return true;
            }
            return false;
        }
    }
}
