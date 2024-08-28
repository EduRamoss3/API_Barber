using Barber.Application.CQRS.Clients.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Client>
    {
        private readonly IUnityOfWork _uof; 
        public RegisterClientCommandHandler(IUnityOfWork uof)
        {
            _uof = uof; 
        }
        public async Task<Client> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            Client client = new Client(request.Name,request.Points, request.Scheduled, request.LastTimeHere,request.Email);
            await _uof.ClientRepository.AddAsync(client);
            await _uof.Commit();
            return client;
            
        }
    }
}
