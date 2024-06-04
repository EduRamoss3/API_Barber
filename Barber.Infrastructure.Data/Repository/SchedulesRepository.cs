using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Infrastructure.Data.Repository
{
    public class SchedulesRepository : ISchedulesRepository
    {
        public Task<bool> AddNewSchedule(Schedules schedule)
        {
            throw new NotImplementedException();
        }

        public Task<Schedules> GetScheduleByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<Schedules> GetScheduleById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Schedules>> GetSchedules()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Schedules>> GetSchedulesByBarberId(int barberId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveSchedule(Schedules schedule)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSchedule(Schedules schedule)
        {
            throw new NotImplementedException();
        }

        public Task UpdateValueForSchedule(Schedules schedule, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
