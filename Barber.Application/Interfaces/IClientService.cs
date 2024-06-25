using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Domain.Entities;

namespace Barber.Application.Interfaces
{
    public interface IClientService
    {
        Task<bool> AddNewClient(ClientRegisterDTO clientDTO);
        Task<bool> RemoveClient(ClientDTO clientDTOt);
        Task<ClientDTO> GetClientById(int id);
        Task<IEnumerable<ClientDTO>> GetClients();
        Task<bool> UpdateClient(ClientDTO clientDTO);
    }
}
