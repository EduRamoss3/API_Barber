using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Domain.Entities;

namespace Barber.Application.Interfaces
{
    public interface IClientService
    {
        Task AddNewClient(ClientRegisterDTO clientDTO);
        Task RemoveClient(ClientDTO clientDTOt);
        Task<Client> GetClientById(int id);
        Task<IEnumerable<Client>> GetClients();
    }
}
