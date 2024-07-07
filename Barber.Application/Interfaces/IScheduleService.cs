using Barber.Application.DTOs;

namespace Barber.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> AddAsync(SchedulesDTO scheduleDTO);
        Task<bool> RemoveAsync(int? id);
        Task<List<SchedulesDTO>> GetAllAsync();
        Task<bool> UpdateAsync(SchedulesDTO scheduleDTO, int? id);
        Task<bool> UpdateValueForAsync(int id, decimal amount);
        Task<SchedulesDTO> GetByIdAsync(int? id);
        Task<List<SchedulesDTO>> GetByBarberIdAsync(int? barberId);
        Task<List<SchedulesDTO>> GetByClientIdAsync(int? clientId);
        Task<bool> EndOpenAsync(int id, bool endOpen);
        Task<bool> GetByDateDisponible(int idBarber, DateTime dateTimeSearch);

    }
}
