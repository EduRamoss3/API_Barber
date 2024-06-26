﻿using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;

namespace Barber.Application.Interfaces
{
    public interface IClientService
    {
        Task<bool> AddAsync(ClientRegisterDTO clientDTO);
        Task<bool> RemoveAsync(int? id);
        Task<ClientDTO> GetByIdAsync(int id);
        Task<IEnumerable<ClientDTO>> GetAllAsync();
        Task<bool> UpdateAsync(ClientDTO clientDTO, int? id);
    }
}
