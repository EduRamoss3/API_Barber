using Barber.Application.CQRS.Barber.Commands;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Barber.Handlers
{
    public class RemoveBarberCommandHandler : IRequestHandler<RemoveBarberCommand, bool>
    {
        private readonly IUnityOfWork _uof;

        public RemoveBarberCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(RemoveBarberCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error, cannot remove barber because data is null");
            }
            var barber = await _uof.BarberRepository.GetByIdAsync(p => p.Id == request.Id);
            if(barber is null)
            {
                return false;
            }
            var result = _uof.BarberRepository.Remove(barber);
            if (result)
            {
                await _uof.Commit();
                return result;
            }
            await  _uof.Dispose();
            return false;   
        }
    }
}
