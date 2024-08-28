
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Data.Repository
{
    public class ClientRepository : Repository <Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context) { }

        public async Task<int> GetIdByEmailAsync(string email)
        {
            var clientId = await _context.Clients.FirstAsync(p => p.Email == email);
            return clientId.Id;
        }

        public async Task UpdatePointsAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if(client is not null)
            {
                client.UpdatePoints();
                _context.Entry(client).Property(p => p.Points).IsModified = true;
            }
        }
    }
}
