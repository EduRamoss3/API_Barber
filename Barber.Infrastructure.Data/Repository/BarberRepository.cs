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

        public async Task AddNewBarber(Domain.Entities.Barber barber)
        {
            if(barber is Barber.Domain.Entities.Barber)
            {
                await _context.Barbers.AddAsync(barber);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entities.Barber>> GetBarbers()
        {
            return await _context.Barbers.ToListAsync();
        }

        public async Task RemoveBarber(Domain.Entities.Barber barber)
        {
            _context.Barbers.Remove(barber);
            await _context.SaveChangesAsync();
        }

        public async Task SetDisponibility(Domain.Entities.Barber barber, bool disponibility)
        {
            _context.Entry(barber).Property(p => p.Disponibility).IsModified = true;
            await _context.SaveChangesAsync();
        }  
    }
}
