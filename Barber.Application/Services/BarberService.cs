using AutoMapper;
using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.CQRS.Barber.Queries;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barber.Application.Services
{
    public class BarberService : IBarberService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BarberService> _logger;

        public BarberService(IMediator mediator, IMapper mapper, ILogger<BarberService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddAsync(BarberRegisterDTO barberRegisterDTO)
        {
            _logger.LogInformation($"Attempting to add barber with name '{barberRegisterDTO.Name}'.");

            var registerBarberCommand = _mapper.Map<RegisterBarberCommand>(barberRegisterDTO);
            var entity = await _mediator.Send(registerBarberCommand);

            if (entity)
            {
                _logger.LogInformation($"Barber '{barberRegisterDTO.Name}' added successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to add barber '{barberRegisterDTO.Name}'.");
                return false;
            }
        }

        public async Task<IEnumerable<BarberDTO>> GetAllAsync()
        {
            _logger.LogInformation($"Attempting to retrieve all barbers.");

            GetBarbersQuery getBarbersQuery = new GetBarbersQuery();
            var barbersEntity = await _mediator.Send(getBarbersQuery);

            _logger.LogInformation($"Retrieved {barbersEntity.Count()} barbers.");

            return _mapper.Map<IEnumerable<BarberDTO>>(barbersEntity);
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            _logger.LogInformation($"Attempting to remove barber with ID '{id}'.");

            var removeBarberCommand = new RemoveBarberCommand(id);
            var result = await _mediator.Send(removeBarberCommand);

            if (result)
            {
                _logger.LogInformation($"Barber with ID '{id}' removed successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to remove barber with ID '{id}'.");
                return false;
            }
        }

        public async Task<bool> SetDisponibilityAsync(int id, bool disponibility)
        {
            _logger.LogInformation($"Attempting to set disponibility for barber with ID '{id}'.");

            UpdateBarberCommand updateBarberCommand = new UpdateBarberCommand(id, disponibility);
            var result = await _mediator.Send(updateBarberCommand);

            if (result)
            {
                _logger.LogInformation($"Disponibility for barber with ID '{id}' updated successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to update disponibility for barber with ID '{id}'.");
                return false;
            }
        }

        public async Task<BarberDTO> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Attempting to retrieve barber with ID '{id}'.");

            GetBarberByIdQuery getBarberByIdQuery = new GetBarberByIdQuery(id);
            var barberEntity = await _mediator.Send(getBarberByIdQuery);

            if (barberEntity != null)
            {
                _logger.LogInformation($"Retrieved barber with ID '{id}' successfully.");
                return _mapper.Map<BarberDTO>(barberEntity);
            }
            else
            {
                _logger.LogWarning($"Barber with ID '{id}' not found.");
                return null;
            }
        }

        public async Task<List<DateTime>> GetIndisponibleDateAsync(int idBarber)
        {
            _logger.LogInformation($"Attempting to retrieve indisponible dates for barber with ID '{idBarber}'.");

            GetIndisponibleDatesQuery getDisponibleDatesQuery = new GetIndisponibleDatesQuery(idBarber);
            var dates = await _mediator.Send(getDisponibleDatesQuery);

            _logger.LogInformation($"Retrieved {dates.Count} indisponible dates for barber with ID '{idBarber}'.");

            return dates;
        }
    }
}
