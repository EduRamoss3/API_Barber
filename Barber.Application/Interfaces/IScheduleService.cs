using Barber.Application.DTOs;
using Barber.Domain.Parameters;

namespace Barber.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> AddAsync(SchedulesDTO scheduleDTO);
        Task<bool> RemoveAsync(int? id);
        Task<List<SchedulesDTO>> GetAllAsync(ParametersToPagination parameters);
        Task<bool> UpdateAsync(SchedulesDTO scheduleDTO, int? id);
        Task<bool> UpdateValueForAsync(int id, decimal amount);
        Task<SchedulesDTO> GetByIdAsync(int? id);
        Task<List<SchedulesDTO>> GetByBarberIdAsync(int? barberId);
        Task<List<SchedulesDTO>> GetByClientIdAsync(int? clientId);
        Task<bool> EndServiceAsync(int id);
        Task<bool> OpenServiceAsync(int id);
        Task<bool> GetByDateDisponible(int idBarber, DateTime dateTimeSearch);

    }
}
