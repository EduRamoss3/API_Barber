using Barber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Interfaces
{
    public interface ISchedulesRepository
    {
        Task<bool> AddNewSchedule(Schedules schedule);
        Task<bool> RemoveSchedule(Schedules schedule);
        Task<IEnumerable<Schedules>> GetAllAsync();
        Task<bool> UpdateSchedule(Schedules schedule);  
        Task UpdateValueForSchedule(Schedules schedule);
        Task<Schedules> GetScheduleById(int id);
        Task<IEnumerable<Schedules>> GetSchedulesByBarberId(int barberId);
        Task<IEnumerable<Schedules>> GetScheduleByClientId(int clientId);
 
    }
}
