using Barber.Domain.Entities;

namespace Barber.Domain.Interfaces
{
    public interface IBarberRepository : IRepository<BarberMain>
    {
        Task<bool> SetDisponibilityAsync(Barber.Domain.Entities.BarberMain barber, bool disponibility);
    }
}
