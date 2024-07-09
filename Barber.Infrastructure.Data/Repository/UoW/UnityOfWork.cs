
using Barber.Domain;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;

namespace Barber.Infrastructure.Data.Repository.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {
        private IBarberRepository _barberRepository;
        private IClientRepository _clientRepository;
        private ISchedulesRepository _schedulesRepository;

        public AppDbContext _context;

        public UnityOfWork(AppDbContext _context)
        {
            this._context = _context;
        }
        public IBarberRepository BarberRepository { get { return _barberRepository = _barberRepository ?? new BarberRepository(_context); } }
        public IClientRepository ClientRepository { get { return _clientRepository = _clientRepository ?? new ClientRepository(_context); } }
        public ISchedulesRepository SchedulesRepository { get { return _schedulesRepository = _schedulesRepository ?? new SchedulesRepository(_context); } }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
        public async Task Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
