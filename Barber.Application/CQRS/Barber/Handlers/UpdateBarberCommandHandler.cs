using Barber.Application.CQRS.Barber.Commands;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Barber.Handlers
{
    public class UpdateBarberCommandHandler : IRequestHandler<UpdateBarberCommand, bool>
    {
        private readonly IUnityOfWork _uof;

        public UpdateBarberCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(UpdateBarberCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                await _uof.Dispose();
                throw new ApplicationException("Error, barber cannot be null");
            }
            var barber = await _uof.BarberRepository.GetByIdAsync(p => p.Id == request.Id);
            if (barber is null)
            {
                await _uof.Dispose();
                throw new ApplicationException("Error, barber no exist");
                
            }
            barber.SetDisponibility(request.Disponibility);
            var result = await _uof.BarberRepository.SetDisponibilityAsync(barber, request.Disponibility);
            await _uof.Commit();
            return result;
        }
    }
}
