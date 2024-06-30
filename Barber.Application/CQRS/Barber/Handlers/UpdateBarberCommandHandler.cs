using Barber.Application.CQRS.Barber.Commands;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Barber.Handlers
{
    public class UpdateBarberCommandHandler : IRequestHandler<UpdateBarberCommand, bool>
    {
        private readonly IBarberRepository _barberRepository;
        public UpdateBarberCommandHandler(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public async Task<bool> Handle(UpdateBarberCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error, barber cannot be null");
            }
            var barber = await _barberRepository.GetBarberByIdAsync(request.Id);
            if (barber is null)
            {
                throw new ApplicationException("Error, barber no exist");
            }
            barber.SetDisponibility(request.Disponibility);
            var result = await _barberRepository.SetDisponibilityAsync(barber, request.Disponibility);

            return result;
        }
    }
}
