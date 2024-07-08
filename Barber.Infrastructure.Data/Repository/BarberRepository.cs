
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;


namespace Barber.Infrastructure.Data.Repository
{
    public class BarberRepository : Repository<BarberMain>, IBarberRepository
    {
        public BarberRepository(AppDbContext context): base(context) { }

        public async Task<bool> SetDisponibilityAsync(Domain.Entities.BarberMain barber, bool disponibility)
        {
            _context.Entry(barber).Property(p => p.Disponibility).IsModified = true;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
