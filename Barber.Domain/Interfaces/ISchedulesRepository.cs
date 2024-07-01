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
        Task<List<Schedules>> GetAllAsync();
        Task<bool> UpdateSchedule(Schedules schedule);  
        Task UpdateValueForSchedule(Schedules schedule);
        Task<Schedules> GetScheduleById(int id);
        Task<List<Schedules>> GetSchedulesByBarberId(int barberId);
        Task<List<Schedules>> GetScheduleByClientId(int clientId);
        Task<bool> EndOrOpenServiceByIdAsync(int id, bool endOrOpen);


    }
}
