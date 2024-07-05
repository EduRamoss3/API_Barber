using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;

namespace Barber.Application.Interfaces
{
    public interface IBarberService 
    {
        Task<bool> AddAsync(BarberRegisterDTO barberDTO);
        Task<bool> RemoveByIdAsync(int id);
        Task<IEnumerable<BarberDTO>> GetAllAsync();
        Task<bool> SetDisponibilityAsync(int id, bool disponibility);
        Task<BarberDTO> GetByIdAsync(int id);
        Task<List<DateTime>> GetDisponibleDateAsync(int idBarber);
    }
}
