using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.Interfaces
{
    public interface IBarberService 
    {
        Task<BarberRegisterDTO> AddNewBarberAsync(BarberRegisterDTO barberDTO);
        Task<BarberDTO> RemoveBarberAsync(BarberDTO barberDTO);
        Task<IEnumerable<BarberDTO>> GetBarbersAsync();
        Task<BarberDTO> SetDisponibilityAsync(BarberDTO barberDTO, bool disponibility);
    }
}
