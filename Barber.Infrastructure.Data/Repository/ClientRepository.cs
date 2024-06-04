using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Infrastructure.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        public Task AddNewClient(Client client)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetClients()
        {
            throw new NotImplementedException();
        }

        public Task RemoveClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
