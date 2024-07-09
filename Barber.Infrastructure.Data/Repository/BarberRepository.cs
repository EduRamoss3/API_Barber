
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;


namespace Barber.Infrastructure.Data.Repository
{
    public class BarberRepository : Repository<BarberMain>, IBarberRepository
    {
        public BarberRepository(AppDbContext context): base(context) { }

        public bool SetDisponibility(Domain.Entities.BarberMain barber, bool disponibility)
        {
            _context.Entry(barber).Property(p => p.Disponibility).IsModified = true;
            return true;
        }

    }
}
