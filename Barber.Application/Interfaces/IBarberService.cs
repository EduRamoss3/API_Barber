﻿using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Domain.Entities;
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
        Task AddNewBarberAsync(BarberRegisterDTO barberDTO);
        Task RemoveBarberByIdAsync(int id);
        Task<IEnumerable<BarberDTO>> GetBarbersAsync();
        Task SetDisponibilityAsync(int id, bool disponibility);
        Task<BarberDTO> GetBarberByIdAsync(int id);
    }
}