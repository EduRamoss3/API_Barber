using AutoMapper;
using Barber.Application.CQRS.Clients.Commands;
using Barber.Application.CQRS.Clients.Queries;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Parameters;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Barber.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IMediator mediator, IMapper mapper, ILogger<ClientService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddAsync(ClientRegisterDTO clientDTO)
        {
            _logger.LogInformation($"Attempting to add client with name '{clientDTO.Name}'.");

            var registerClientCommand = _mapper.Map<RegisterClientCommand>(clientDTO);
            var entity = await _mediator.Send(registerClientCommand);

            if (entity != null)
            {
                _logger.LogInformation($"Client '{clientDTO.Name}' added successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to add client '{clientDTO.Name}'.");
                return false;
            }
        }

        public async Task<ClientDTO> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Attempting to retrieve client with ID '{id}'.");

            var getClientByIdQuery = new GetClientByIdQuery(id);
            var entity = await _mediator.Send(getClientByIdQuery);

            if (entity != null)
            {
                _logger.LogInformation($"Retrieved client with ID '{id}' successfully.");
                return _mapper.Map<ClientDTO>(entity);
            }
            else
            {
                _logger.LogWarning($"Client with ID '{id}' not found.");
                return null;
            }
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync(GetParametersPagination ParametersPagination)
        {
            _logger.LogInformation($"Attempting to retrieve all clients.");

            var getClientsQuery = new GetClientsQuery(ParametersPagination);
            var entities = await _mediator.Send(getClientsQuery);

            _logger.LogInformation($"Retrieved {entities.Count()} clients.");

            return _mapper.Map<IEnumerable<ClientDTO>>(entities);
        }

        public async Task<bool> RemoveAsync(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("ID parameter for removing client is null.");
                return false;
            }

            _logger.LogInformation($"Attempting to remove client with ID '{id.Value}'.");

            var removeClientCommand = new RemoveClientCommand(id.Value);
            var entity = await _mediator.Send(removeClientCommand);

            if (entity)
            {
                _logger.LogInformation($"Client with ID '{id.Value}' removed successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to remove client with ID '{id.Value}'.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(ClientDTO clientDTO, int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("ID parameter for updating client is null.");
                return false;
            }

            _logger.LogInformation($"Attempting to update client with ID '{id.Value}'.");

            var updateClientCommand = _mapper.Map<UpdateClientCommand>(clientDTO);
            updateClientCommand.Id = id.Value;
            var result = await _mediator.Send(updateClientCommand);

            if (result)
            {
                _logger.LogInformation($"Client with ID '{id.Value}' updated successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to update client with ID '{id.Value}'.");
                return false;
            }
        }

        public async Task UpdatePointsAsync(int id)
        {
            _logger.LogInformation($"Attempting to update points for client with ID '{id}'.");

            UpdatePointsClientCommand cmd = new UpdatePointsClientCommand(id);
            await _mediator.Send(cmd);

            _logger.LogInformation($"Points updated for client with ID '{id}' successfully.");
        }
    }
}
