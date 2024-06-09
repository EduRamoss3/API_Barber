using Barber.Application.DTOs;
using Barber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> AddNewSchedule(SchedulesDTO scheduleDTO);
        Task<bool> RemoveSchedule(SchedulesDTO scheduleDTO);
        Task<IEnumerable<SchedulesDTO>> GetSchedules();
        Task<bool> UpdateSchedule(SchedulesDTO scheduleDTO);
        Task UpdateValueForSchedule(SchedulesDTO scheduleDTO);
        Task<SchedulesDTO> GetScheduleById(int id);
        Task<IEnumerable<SchedulesDTO>> GetSchedulesByBarberId(int barberId);
        Task<SchedulesDTO> GetScheduleByClientId(int clientId);
    }
}
