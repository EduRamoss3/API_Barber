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
        Task AddAsync(Client client);
        Task RemoveAsync(Client client);
        Task<Client> GetByIdAsync(int id);
        Task<IEnumerable<Client>> GetAllAsync();  
        Task<bool> Update(Client client);
    }
}
