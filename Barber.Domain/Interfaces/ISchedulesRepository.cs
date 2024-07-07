using Barber.Domain.Entities;

namespace Barber.Domain.Interfaces
{
    public interface ISchedulesRepository
    {
        Task<bool> AddAsync(Schedules schedule);
        Task<bool> RemoveAsync(Schedules schedule);
        Task<List<Schedules>> GetAllAsync();
        Task<bool> UpdateAsync(Schedules schedule);  
        Task UpdateValueForAsync(Schedules schedule);
        Task<Schedules> GetByIdAsync(int id);
        Task<List<Schedules>> GetByBarberIdAsync(int barberId);
        Task<List<Schedules>> GetByClientIdAsync(int clientId);
        Task<bool> EndOrOpenServiceByIdAsync(int id, bool endOrOpen);
        Task<List<DateTime>> GetIndisponibleDatesByBarberId(int barberId);
        Task<bool> IsDisponibleInThisDate(int barberId, DateTime dateTime);


    }
}
