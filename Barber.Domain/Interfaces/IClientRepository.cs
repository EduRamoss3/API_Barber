using Barber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task AddNewClient(Client client);
        Task RemoveClient(Client client);
        Task<Client> GetClientById(int id);
        Task<IEnumerable<Client>> GetClients();  
        Task<bool> Update(Client client);
    }
}
