using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Data.Repository
{
    public class SchedulesRepository : ISchedulesRepository
    {
        private readonly AppDbContext _context;
        public SchedulesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Schedules schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return Task.CompletedTask.IsCompleted;
        }

        public async Task<List<Schedules>> GetByClientIdAsync(int clientId)
        {
            return await _context.Schedules.Where(p => p.IdClient == clientId).ToListAsync();
        }

        public async Task<Schedules> GetByIdAsync(int id)
        {
            return await _context.Schedules.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Schedules>> GetAllAsync()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async Task<List<Schedules>> GetByBarberIdAsync(int barberId)
        {
            return await _context.Schedules.Where(p => p.IdBarber == barberId).ToListAsync();
        }

        public async Task<bool> RemoveAsync(Schedules schedule)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return Task.CompletedTask.IsCompleted;
        }

        public async Task<bool> UpdateAsync(Schedules schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return Task.CompletedTask.IsCompleted;
        }

        public async Task UpdateValueForAsync(Schedules schedule)
        {
            _context.Entry(schedule).Property(p => p.ValueForService).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EndOrOpenServiceByIdAsync(int id, bool endOrOpen)
        {
            var schedule = await GetByIdAsync(id);
            if(schedule is Schedules)
            {
                schedule.SetIsClose(endOrOpen);
                _context.Entry(schedule).Property(p => p.IsFinalized).IsModified = true;
                await _context.SaveChangesAsync();
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
