
namespace Barber.Domain.Interfaces
{
    public interface IUnityOfWork
    {
        IBarberRepository BarberRepository { get; }
        IClientRepository ClientRepository { get; }
        ISchedulesRepository SchedulesRepository { get; }
        Task Commit();
        Task Dispose();
    }
}
