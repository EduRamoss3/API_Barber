﻿using AutoMapper;
using Barber.Application.CQRS.Schedule.Commands;
using Barber.Application.CQRS.Schedule.Queries;
using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using Barber.Domain.Interfaces;
using Barber.Domain.Parameters;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barber.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ScheduleService> _logger;
        private readonly ISchedulesRepository _schedulesRepository;

        public ScheduleService(IMediator mediator, IMapper mapper, ILogger<ScheduleService> logger, ISchedulesRepository repository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _schedulesRepository = repository;
        }

        public async Task<List<SchedulesDTO>> GetWithData()
        {
            var query = new GetWithData();
            var entity = await _mediator.Send(query);
            if(entity is null)
            {
                return new List<SchedulesDTO>();
            }
            return _mapper.Map<List<SchedulesDTO>>(entity);
        }
        public async Task<bool> AddAsync(SchedulesDTO scheduleDTO)
        {
            _logger.LogInformation($"Attempting to add schedule for client ID '{scheduleDTO.IdClient}'.");

            var addScheduleCommand = _mapper.Map<AddScheduleCommand>(scheduleDTO);
            var entity = await _mediator.Send(addScheduleCommand);

            if (entity != null)
            {
                _logger.LogInformation($"Schedule added successfully for client ID '{scheduleDTO.IdClient}'.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to add schedule for client ID '{scheduleDTO.IdClient}'.");
                return false;
            }
        }

        public async Task<List<SchedulesDTO>> GetByClientIdAsync(int? clientId)
        {
            _logger.LogInformation($"Attempting to retrieve schedules for client ID '{clientId}'.");

            var getScheduleByClientId = new GetScheduleByClientIdQuery(clientId.Value);
            var schedules = await _mediator.Send(getScheduleByClientId);

            if(schedules is null)
            {
                return null;
            }
            _logger.LogInformation($"Retrieved {schedules.Count} schedules for client ID '{clientId}'.");

            return _mapper.Map<List<SchedulesDTO>>(schedules);
        }

        public async Task<SchedulesDTO> GetByIdAsync(int? id)
        {
            _logger.LogInformation($"Attempting to retrieve schedule with ID '{id}'.");

            var getScheduleById = new GetScheduleByIdQuery(id.Value);
            var schedule = await _mediator.Send(getScheduleById);

            if (schedule != null)
            {
                _logger.LogInformation($"Retrieved schedule with ID '{id}' successfully.");
                return _mapper.Map<SchedulesDTO>(schedule);
            }
            else
            {
                _logger.LogWarning($"Schedule with ID '{id}' not found.");
                return null;
            }
        }

        public async Task<List<SchedulesDTO>> GetAllAsync(ParametersToPagination ParametersPagination)
        {
            _logger.LogInformation($"Attempting to retrieve all schedules.");

            var getSchedules = new GetSchedulesQuery(ParametersPagination);
            var schedules = await _mediator.Send(getSchedules);

            _logger.LogInformation($"Retrieved {schedules.Count()} schedules.");

            return _mapper.Map<List<SchedulesDTO>>(schedules);
        }

        public async Task<List<SchedulesDTO>> GetByBarberIdAsync(int? barberId)
        {
            _logger.LogInformation($"Attempting to retrieve schedules for barber ID '{barberId}'.");

            var getScheduleByBarberId = new GetSchedulesByBarberIdQuery(barberId.Value);
            var schedules = await _mediator.Send(getScheduleByBarberId);

            _logger.LogInformation($"Retrieved {schedules.Count} schedules for barber ID '{barberId}'.");

            return _mapper.Map<List<SchedulesDTO>>(schedules);
        }

        public async Task<bool> RemoveAsync(int? id)
        {
            _logger.LogInformation($"Attempting to remove schedule with ID '{id}'.");

            var removeScheduleCommand = new RemoveScheduleCommand(id.Value);
            var entity = await _mediator.Send(removeScheduleCommand);

            if (entity)
            {
                _logger.LogInformation($"Schedule with ID '{id}' removed successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to remove schedule with ID '{id}'.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(SchedulesDTO scheduleDTO, int? id)
        {
            _logger.LogInformation($"Attempting to update schedule with ID '{id}'.");

            var updateScheduleCommand = _mapper.Map<UpdateScheduleCommand>(scheduleDTO);
            updateScheduleCommand.Id = id.Value;
            var entity = await _mediator.Send(updateScheduleCommand);

            if (entity)
            {
                _logger.LogInformation($"Schedule with ID '{id}' updated successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to update schedule with ID '{id}'.");
                return false;
            }
        }

        public async Task<bool> UpdateValueForAsync(int id, decimal amount)
        {
            _logger.LogInformation($"Attempting to update value for schedule with ID '{id}'.");

            UpdateValueForScheduleCommand updateValueForScheduleCommand = new UpdateValueForScheduleCommand(id, amount);
            var entity = await _mediator.Send(updateValueForScheduleCommand);

            if (entity != null)
            {
                _logger.LogInformation($"Value updated for schedule with ID '{id}' successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to update value for schedule with ID '{id}'.");
                return false;
            }
        }

        public async Task<bool> EndServiceAsync(int id)
        {
            _logger.LogInformation($"Attempting to update end service for schedule with ID '{id}'.");

            PatchEndServiceScheduleCommand patchEndServiceScheduleCommand = new PatchEndServiceScheduleCommand(id);
            var entity = await _mediator.Send(patchEndServiceScheduleCommand);

            if (entity is not null)
            {
                _logger.LogInformation($"End updated for schedule with ID '{id}' successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to update end open for schedule with ID '{id}'.");
                return false;

            }

        }

        public async Task<bool> OpenServiceAsync(int id)
        {
            _logger.LogInformation($"Attempting to update open service for schedule with ID '{id}'.");

            PatchOpenServiceScheduleCommand patchOpenServiceScheduleCommand = new PatchOpenServiceScheduleCommand(id);
            var entity = await _mediator.Send(patchOpenServiceScheduleCommand);

            if (entity is not null)
            {
                _logger.LogInformation($"End open updated for schedule with ID '{id}' successfully.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to update open for schedule with ID '{id}'.");
                return false;
            }
        }

        public async Task<bool> GetByDateDisponible(int idBarber, DateTime dateTimeSearch)
        {
            _logger.LogInformation($"Attempting to retrieve disponibility for barber ID '{idBarber}' on date '{dateTimeSearch}'.");

            GetDisponibleScheduleByDateQuery getDisponibleScheduleByDateQuery = new GetDisponibleScheduleByDateQuery(idBarber, dateTimeSearch);
            var entity = await _mediator.Send(getDisponibleScheduleByDateQuery);

            if (entity)
            {
                _logger.LogInformation($"Disponibility found for barber ID '{idBarber}' on date '{dateTimeSearch}'.");
            }
            else
            {
                _logger.LogWarning($"No disponibility found for barber ID '{idBarber}' on date '{dateTimeSearch}'.");
            }

            return entity;
        }
    }
}
