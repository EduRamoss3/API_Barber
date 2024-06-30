using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Infrastructure.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task RemoveAsync(Client client)
        {
             _context.Clients.Remove(client);
             await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(Client client)
        {
            if(client is not Client)
            {
                return false;
            }
            var cliente = await GetByIdAsync(client.Id);
            cliente.Update(client.Name, client.Points, client.Scheduled, client.LastTimeHere);
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
