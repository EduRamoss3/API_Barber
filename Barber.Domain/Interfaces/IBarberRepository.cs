using Barber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Interfaces
{
    public interface IBarberRepository
    {
        Task AddNewBarber(Barber.Domain.Entities.Barber barber);
        Task RemoveBarber(Barber.Domain.Entities.Barber barber);
        Task<IEnumerable<Barber.Domain.Entities.Barber>> GetBarbers();  
        Task SetDisponibility(Barber.Domain.Entities.Barber barber, bool disponibility);
    }
}
