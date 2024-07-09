using Barber.Domain.Entities;

namespace Barber.Domain.Interfaces
{
    public interface IBarberRepository : IRepository<BarberMain>
    {
       bool SetDisponibility(Barber.Domain.Entities.BarberMain barber, bool disponibility);
    }
}
