using AutoMapper;
using Barber.Application.CQRS.Schedule.Commands;
using Barber.Application.CQRS.Schedule.Queries;
using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using MediatR;

namespace Barber.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ScheduleService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    
        public async Task<bool> AddAsync(SchedulesDTO scheduleDTO)
        {
            var addScheduleCommand = _mapper.Map<AddScheduleCommand>(scheduleDTO);
            var entity = await _mediator.Send(addScheduleCommand);
            return entity is not null ? true : false;
        }

        public async Task<List<SchedulesDTO>> GetByClientIdAsync(int? clientId)
        {
            var getScheduleByClientId = new GetScheduleByClientIdQuery(clientId.Value);
            var schedules = await _mediator.Send(getScheduleByClientId);
            return _mapper.Map<List<SchedulesDTO>>(schedules);
        }

        public async Task<SchedulesDTO> GetByIdAsync(int? id)
        {
            var getScheduleById = new GetScheduleByIdQuery(id.Value);
            var schedules = await _mediator.Send(getScheduleById);
            return _mapper.Map<SchedulesDTO>(schedules);
        }

        public async Task<List<SchedulesDTO>> GetAllAsync()
        {
            var getSchedules = new GetSchedulesQuery();
            var schedules = await _mediator.Send(getSchedules);
            return _mapper.Map<List<SchedulesDTO>>(schedules);
        }

        public async Task<List<SchedulesDTO>> GetByBarberIdAsync(int? barberId)
        {
            var getScheduleByBarberId = new GetSchedulesByBarberIdQuery(barberId.Value);
            var schedules = await _mediator.Send(getScheduleByBarberId);
            return _mapper.Map<List<SchedulesDTO>>(schedules);
        }

        public async Task<bool> RemoveAsync(int? id)
        {
            var removeScheduleCommand = new RemoveScheduleCommand(id.Value);
            var entity = await _mediator.Send(removeScheduleCommand);
            return entity is not null ? true : false;
        }

        public async Task<bool> UpdateAsync(SchedulesDTO scheduleDTO, int? id)
        {
            var updateScheduleCommand = _mapper.Map<UpdateScheduleCommand>(scheduleDTO);
            updateScheduleCommand.Id = id.Value;
            var entity = await _mediator.Send(updateScheduleCommand);
            return entity is not null ? true : false;
        }

        public async Task<bool> UpdateValueForAsync(int id, decimal amount)
        {
            UpdateValueForScheduleCommand updateValueForScheduleCommand = new UpdateValueForScheduleCommand(id, amount);
            var entity = await _mediator.Send(updateValueForScheduleCommand);
            return entity is not null ? true : false;
        }
        public async Task<bool> EndOpenAsync(int id, bool endOpen)
        {
            PatchEndOpenServiceScheduleCommand patchEndOpenServiceScheduleCommand = new PatchEndOpenServiceScheduleCommand(id, endOpen);
            var entity = await _mediator.Send(patchEndOpenServiceScheduleCommand);
            return entity;
        }

        public async Task<bool> GetByDateDisponible(int idBarber, DateTime dateTimeSearch)
        {
            GetDisponibleScheduleByDateQuery getDisponibleScheduleByDateQuery = new GetDisponibleScheduleByDateQuery(idBarber, dateTimeSearch);
            var entity = await _mediator.Send(getDisponibleScheduleByDateQuery);
            return entity;
        }
    }
}
