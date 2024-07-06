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
        public async Task<bool> AddNewSchedule(Schedules schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return Task.CompletedTask.IsCompleted;
        }

        public async Task<List<Schedules>> GetScheduleByClientId(int clientId)
        {
            return await _context.Schedules.Where(p => p.IdClient == clientId).ToListAsync();
        }

        public async Task<Schedules> GetScheduleById(int id)
        {
            return await _context.Schedules.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Schedules>> GetAllAsync()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async Task<List<Schedules>> GetSchedulesByBarberId(int barberId)
        {
            return await _context.Schedules.Where(p => p.IdBarber == barberId).ToListAsync();
        }

        public async Task<bool> RemoveSchedule(Schedules schedule)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return Task.CompletedTask.IsCompleted;
        }

        public async Task<bool> UpdateSchedule(Schedules schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return Task.CompletedTask.IsCompleted;
        }

        public async Task UpdateValueForSchedule(Schedules schedule)
        {
            _context.Entry(schedule).Property(p => p.ValueForService).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EndOrOpenServiceByIdAsync(int id, bool endOrOpen)
        {
            var schedule = await GetScheduleById(id);
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
    }
}
