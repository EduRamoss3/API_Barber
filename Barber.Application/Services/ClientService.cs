using AutoMapper;
using Barber.Application.CQRS.Clients.Commands;
using Barber.Application.CQRS.Clients.Queries;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
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
    
        public async Task<bool> AddAsync(ClientRegisterDTO clientDTO)
        {
            var registerClientCommand = _mapper.Map<RegisterClientCommand>(clientDTO);
            var entity = await _mediator.Send(registerClientCommand);
            return entity is not null ? true : false;
        }

        public async Task<ClientDTO> GetByIdAsync(int id)
        {
            var getClientByIdQuery = new GetClientByIdQuery(id);
            var entity = await _mediator.Send(getClientByIdQuery);
            return _mapper.Map<ClientDTO>(entity);
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            var getClientsQuery = new GetClientsQuery();
            var entities = await _mediator.Send(getClientsQuery);
            return _mapper.Map<IEnumerable<ClientDTO>>(entities);
        }

        public async Task<bool> RemoveAsync(int? id)
        {
            var removeClientCommand = new RemoveClientCommand(id.Value);
            var entity = await _mediator.Send(removeClientCommand);
            return entity is not null ? true : false;
        }
        public async Task<bool> UpdateAsync(ClientDTO clientDTO, int? id)
        {
            var updateClientCommand = _mapper.Map<UpdateClientCommand>(clientDTO);
            updateClientCommand.Id = id.Value;
            return  await _mediator.Send(updateClientCommand);
        }
    }
}
