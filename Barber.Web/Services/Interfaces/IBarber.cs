using Barber.Web.Models;

namespace Barber.Web.Services.Interfaces
{
    public interface IBarber
    {
        Task AddNewBarberAsync(BarberVO barberVO);
        Task RemoveBarberByIdAsync(int id);
        Task<IEnumerable<BarberVO>> GetBarbersAsync();
        Task SetDisponibilityAsync(BarberVO barberVO, bool disponibility);
    }
}
