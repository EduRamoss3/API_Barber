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
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Client>
    {
        private readonly IClientRepository _clientRepository;
        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Error, client not exist");
            }
            var client = await _clientRepository.GetClientById(request.Id);
            client.Update(request.Name, request.Points, request.Scheduled, request.LastTimeHere);
            return client;
            
        }
    }
}
