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

        public async Task<IEnumerable<Schedules>> GetScheduleByClientId(int clientId)
        {
            return await _context.Schedules.Where(p => p.IdClient == clientId).ToListAsync();
        }

        public async Task<Schedules> GetScheduleById(int id)
        {
            return await _context.Schedules.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Schedules>> GetSchedules()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async Task<IEnumerable<Schedules>> GetSchedulesByBarberId(int barberId)
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
    }
}
