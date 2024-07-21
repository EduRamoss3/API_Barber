using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Data.Repository
{
    public class SchedulesRepository : Repository<Schedules>, ISchedulesRepository
    {
        public SchedulesRepository(AppDbContext context) : base(context) { }

     
        public async Task<List<Schedules>> GetByClientIdAsync(int clientId)
        {
            return await _context.Schedules.Where(p => p.IdClient == clientId).ToListAsync();
        }

        public async Task<List<Schedules>> GetByBarberIdAsync(int barberId)
        {
           return await _context.Schedules.Where(p => p.IdBarber == barberId).ToListAsync();  
        }

        public void UpdateValueFor(Schedules schedule)
        {
            _context.Entry(schedule).Property(p => p.ValueForService).IsModified = true;
        }

        public async Task<bool> EndOrOpenServiceByIdAsync(int id, bool endOrOpen)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if(schedule is Schedules)
            {
                schedule.SetIsClose(endOrOpen);
                _context.Entry(schedule).Property(p => p.IsFinalized).IsModified = true;
                return true;
            }
            return false;
        }

        public async Task<List<DateTime>> GetIndisponibleDatesByBarberId(int barberId)
        {
            var listSchedules = await _context.Schedules
                .Where(p => p.IdBarber == barberId
                && !(p.IsFinalized))
                .Select(d => d.DateSchedule)
                .OrderBy(p => p.Month).ToListAsync();
            return listSchedules;
        }

        public async Task<bool> IsDisponibleInThisDate(int barberId, DateTime dateTime)
        {
            List<DateTime> dateTimes = await _context.Schedules.Where(p => p.IdBarber == barberId).Select(p => p.DateSchedule).ToListAsync();
            string targetTime = dateTime.ToString("dd/MM/yyyy HH:mm");
            var exist = dateTimes.Any(p => p.ToString("dd/MM/yyyy HH:mm").Equals(targetTime));

            if (exist)
            {
                return false;
            }
            return true;
        }

       
    }
}
