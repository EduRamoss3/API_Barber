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
        Task<BarberMain> AddNewBarberAsync(Barber.Domain.Entities.BarberMain barber);
        Task<bool> RemoveBarberAsync(Barber.Domain.Entities.BarberMain barber);
        Task<IEnumerable<Barber.Domain.Entities.BarberMain>> GetBarbersAsync();  
        Task<bool> SetDisponibilityAsync(Barber.Domain.Entities.BarberMain barber, bool disponibility);
        Task<BarberMain> GetBarberByIdAsync(int id);
        Task<bool> UpdateAsync(BarberMain barber);

    }
}
