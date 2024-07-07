using Barber.Domain.Entities;

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
        Task<List<DateTime>> GetIndisponibleDatesByBarberId(int barberId);
        Task<bool> IsDisponibleInThisDate(int barberId, DateTime dateTime);


    }
}
