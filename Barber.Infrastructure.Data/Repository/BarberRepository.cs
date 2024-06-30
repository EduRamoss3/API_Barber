using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Infrastructure.Data.Repository
{
    public class BarberRepository : IBarberRepository
    {
        private readonly AppDbContext _context;
        public BarberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BarberMain> AddAsync(Domain.Entities.BarberMain barber)
        {
            if (barber is Barber.Domain.Entities.BarberMain)
            {
                await _context.Barbers.AddAsync(barber);
                await _context.SaveChangesAsync();
            }
            return barber;
        }

        public async Task<IEnumerable<Domain.Entities.BarberMain>> GetAllAsync()
        {
            return await _context.Barbers.ToListAsync();
        }

        public async Task<bool> RemoveAsync(Domain.Entities.BarberMain barber)
        {
            if(barber is null)
            {
                return false;
            }
            _context.Barbers.Remove(barber);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetDisponibilityAsync(Domain.Entities.BarberMain barber, bool disponibility)
        {
            _context.Entry(barber).Property(p => p.Disponibility).IsModified = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BarberMain> GetBarberByIdAsync(int id)
        {
            return await _context.Barbers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> UpdateAsync(BarberMain barber)
        {
            _context.Barbers.Update(barber);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
