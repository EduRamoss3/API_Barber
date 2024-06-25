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
    
        public async Task<bool> AddNewClient(ClientRegisterDTO clientDTO)
        {
            var registerClientCommand = _mapper.Map<RegisterClientCommand>(clientDTO);
            var entity = await _mediator.Send(registerClientCommand);
            return entity is not null ? true : false;
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            var getClientByIdQuery = new GetClientByIdQuery(id);
            var entity = await _mediator.Send(getClientByIdQuery);
            return _mapper.Map<ClientDTO>(entity);
        }

        public async Task<IEnumerable<ClientDTO>> GetClients()
        {
            var getClientsQuery = new GetClientsQuery();
            var entities = await _mediator.Send(getClientsQuery);
            return _mapper.Map<IEnumerable<ClientDTO>>(entities);
        }

        public async Task<bool> RemoveClient(ClientDTO clientDTO)
        {
            var removeClientCommand = _mapper.Map<RemoveClientCommand>(clientDTO);
            var entity = await _mediator.Send(removeClientCommand);
            return entity is not null ? true : false;
        }
        public async Task<bool> UpdateClient(ClientDTO clientDTO)
        {
            var updateClientCommand = _mapper.Map<UpdateClientCommand>(clientDTO);
            return  await _mediator.Send(updateClientCommand);
        }
    }
}
