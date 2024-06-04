using Barber.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Infrastructure.Data.Repository
{
    public class BarberRepository : IBarberRepository
    {
        public Task AddNewBarber(Domain.Entities.Barber barber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Barber>> GetBarbers()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBarber(Domain.Entities.Barber barber)
        {
            throw new NotImplementedException();
        }

        public Task SetDisponibility(bool disponibility)
        {
            throw new NotImplementedException();
        }

        public Task SetTimeClose()
        {
            throw new NotImplementedException();
        }

        public Task SetTimeFree()
        {
            throw new NotImplementedException();
        }
    }
}
