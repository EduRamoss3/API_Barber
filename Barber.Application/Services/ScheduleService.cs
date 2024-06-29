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
    
        public async Task<bool> AddNewSchedule(SchedulesDTO scheduleDTO)
        {
            var addScheduleCommand = _mapper.Map<AddScheduleCommand>(scheduleDTO);
            var entity = await _mediator.Send(addScheduleCommand);
            return entity is not null ? true : false;
        }

        public async Task<IEnumerable<SchedulesDTO>> GetScheduleByClientId(int? clientId)
        {
            var getScheduleByClientId = new GetScheduleByClientIdQuery(clientId.Value);
            var schedules = await _mediator.Send(getScheduleByClientId);
            return _mapper.Map<IEnumerable<SchedulesDTO>>(schedules);
        }

        public async Task<SchedulesDTO> GetScheduleById(int? id)
        {
            var getScheduleById = new GetScheduleByIdQuery(id.Value);
            var schedules = await _mediator.Send(getScheduleById);
            return _mapper.Map<SchedulesDTO>(schedules);
        }

        public async Task<IEnumerable<SchedulesDTO>> GetSchedules()
        {
            var getSchedules = new GetSchedulesQuery();
            var schedules = await _mediator.Send(getSchedules);
            return _mapper.Map<IEnumerable<SchedulesDTO>>(schedules);
        }

        public async Task<IEnumerable<SchedulesDTO>> GetSchedulesByBarberId(int? barberId)
        {
            var getScheduleByBarberId = new GetSchedulesByBarberIdQuery(barberId.Value);
            var schedules = await _mediator.Send(getScheduleByBarberId);
            return _mapper.Map<IEnumerable<SchedulesDTO>>(schedules);
        }

        public async Task<bool> RemoveSchedule(SchedulesDTO scheduleDTO)
        {
            var removeScheduleCommand = new RemoveScheduleCommand(scheduleDTO.Id);
            var entity = await _mediator.Send(removeScheduleCommand);
            return entity is not null ? true : false;
        }

        public async Task<bool> UpdateSchedule(SchedulesDTO scheduleDTO)
        {
            var updateScheduleCommand = _mapper.Map<UpdateScheduleCommand>(scheduleDTO);
            var entity = await _mediator.Send(updateScheduleCommand);
            return entity is not null ? true : false;
        }

        public async Task<bool> UpdateValueForSchedule(int id, decimal amount)
        {
            UpdateValueForScheduleCommand updateValueForScheduleCommand = new UpdateValueForScheduleCommand(id, amount);
            var entity = await _mediator.Send(updateValueForScheduleCommand);
            return entity is not null ? true : false;
        }

    }
}
