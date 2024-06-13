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
    public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Client>
    {
        private readonly IClientRepository _clientRepository;
        public RegisterClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;   
        }
        public async Task<Client> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            Client client = new Client(request.Name,request.Points, request.Scheduled, request.LastTimeHere);
            await _clientRepository.AddNewClient(client);
            return client;
            
        }
    }
}
