using Barber.Domain.Entities;

namespace Barber.Domain.Interfaces
{
    public interface ISchedulesRepository : IRepository<Schedules>
    {
        void UpdateValueFor(Schedules schedule);

        Task<List<Schedules>> GetByBarberIdAsync(int barberId);

        Task<List<Schedules>> GetByClientIdAsync(int clientId);

        Task<bool> EndOrOpenServiceByIdAsync(int id, bool endOrOpen);

        Task<List<DateTime>> GetIndisponibleDatesByBarberId(int barberId);

        Task<bool> IsDisponibleInThisDate(int barberId, DateTime dateTime);

        Task<List<Schedules>> GetWithData();

        Task<Schedules> GetByIdDataAsync(int id);

    }
}
