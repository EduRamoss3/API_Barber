using AutoMapper;
using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    
        public Task<bool> AddNewSchedule(SchedulesDTO scheduleDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SchedulesDTO>> GetScheduleByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<SchedulesDTO> GetScheduleById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SchedulesDTO>> GetSchedules()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SchedulesDTO>> GetSchedulesByBarberId(int barberId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveSchedule(SchedulesDTO scheduleDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSchedule(SchedulesDTO scheduleDTO)
        {
            throw new NotImplementedException();
        }

        public Task UpdateValueForSchedule(SchedulesDTO scheduleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
