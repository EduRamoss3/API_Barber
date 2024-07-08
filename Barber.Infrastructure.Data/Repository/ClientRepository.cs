
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;

namespace Barber.Infrastructure.Data.Repository
{
    public class ClientRepository : Repository <Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context) { }   

        public async Task UpdatePointsAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if(client is not null)
            {
                client.UpdatePoints();
                _context.Entry(client).Property(p => p.Points).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
