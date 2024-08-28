

using Barber.Domain.Entities;

namespace Barber.Domain.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task UpdatePointsAsync(int id);
        Task<int> GetIdByEmailAsync(string email);
    }
}
