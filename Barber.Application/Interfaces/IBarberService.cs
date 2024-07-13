using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Domain.Parameters;

namespace Barber.Application.Interfaces
{
    public interface IBarberService 
    {
        Task<bool> AddAsync(BarberRegisterDTO barberDTO);
        Task<bool> RemoveByIdAsync(int id);
        Task<IEnumerable<BarberDTO>> GetAllAsync(ParametersToPagination parameters);
        Task<bool> SetDisponibilityAsync(int id, bool disponibility);
        Task<BarberDTO> GetByIdAsync(int id);
        Task<List<DateTime>> GetIndisponibleDateAsync(int idBarber);
    }
}
