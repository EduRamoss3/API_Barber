using AutoMapper;
using Barber.Application.CQRS.Clients.Commands;
using Barber.Application.CQRS.Clients.Queries;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using MediatR;

namespace Barber.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ClientService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    
        public async Task AddNewClient(ClientRegisterDTO clientDTO)
        {
            var registerClientCommand = _mapper.Map<RegisterClientCommand>(clientDTO);
            await _mediator.Send(registerClientCommand);
        }

        public async Task<Client> GetClientById(int id)
        {
            var getClientByIdQuery = new GetClientByIdQuery(id);
            return await _mediator.Send(getClientByIdQuery);
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            var getClientsQuery = new GetClientsQuery();
            return await _mediator.Send(getClientsQuery);
        }

        public async Task RemoveClient(ClientDTO clientDTO)
        {
            var removeClientCommand = _mapper.Map<RemoveClientCommand>(clientDTO);
            await _mediator.Send(removeClientCommand);
        }
    }
}
