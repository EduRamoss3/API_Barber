using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class RemoveClientCommandHandler : IRequestHandler<RemoveClientCommand, Client>
    {
        private readonly IUnityOfWork _uof; 
        public RemoveClientCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Client> Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                await _uof.Dispose();
                throw new ApplicationException("Request dont exist");
            }
            var client = await _uof.ClientRepository.GetByIdAsync(p => p.Id == request.Id);
            if(client is null)
            {
                await _uof.Dispose();
                throw new ApplicationException("Client dont exist!");
            }
            await _uof.ClientRepository.RemoveAsync(client);
            await _uof.Commit();
            return client;
        }
    }
}
