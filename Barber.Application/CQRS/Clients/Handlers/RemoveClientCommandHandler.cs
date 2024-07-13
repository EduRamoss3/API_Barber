using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Domain.Validation;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class RemoveClientCommandHandler : IRequestHandler<RemoveClientCommand, bool>
    {
        private readonly IUnityOfWork _uof; 
        public RemoveClientCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                await _uof.Dispose();
                throw new ApplicationException("Request dont exist");
            }
            var client = await _uof.ClientRepository.GetByIdAsync(p => p.Id == request.Id);
            if (client is null)
            {
                return false;
            }

            _uof.ClientRepository.Remove(client);
            await _uof.Commit();
            return true;
        }
    }
}
