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
        Task AddNewSchedule(SchedulesDTO scheduleDTO);
        Task<SchedulesDTO> RemoveSchedule(SchedulesDTO scheduleDTO);
        Task<IEnumerable<SchedulesDTO>> GetSchedules();
        Task UpdateSchedule(SchedulesDTO scheduleDTO);
        Task UpdateValueForSchedule(SchedulesDTO scheduleDTO);
        Task<SchedulesDTO> GetScheduleById(int? id);
        Task<IEnumerable<SchedulesDTO>> GetSchedulesByBarberId(int? barberId);
        Task<IEnumerable<SchedulesDTO>> GetScheduleByClientId(int? clientId);
    }
}
