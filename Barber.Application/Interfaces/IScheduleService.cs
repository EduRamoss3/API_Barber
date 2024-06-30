using Barber.Application.DTOs;

namespace Barber.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> AddAsync(SchedulesDTO scheduleDTO);
        Task<bool> RemoveAsync(int? id);
        Task<IEnumerable<SchedulesDTO>> GetAllAsync();
        Task<bool> UpdateAsync(SchedulesDTO scheduleDTO, int? id);
        Task<bool> UpdateValueForAsync(int id, decimal amount);
        Task<SchedulesDTO> GetByIdAsync(int? id);
        Task<IEnumerable<SchedulesDTO>> GetByBarberIdAsync(int? barberId);
        Task<IEnumerable<SchedulesDTO>> GetByClientIdAsync(int? clientId);
    }
}
