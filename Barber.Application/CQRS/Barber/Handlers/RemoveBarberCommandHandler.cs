using Barber.Application.CQRS.Barber.Commands;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Barber.Handlers
{
    public class RemoveBarberCommandHandler : IRequestHandler<RemoveBarberCommand, bool>
    {
        private readonly IBarberRepository _barberRepository;
        public RemoveBarberCommandHandler(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public async Task<bool> Handle(RemoveBarberCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error, cannot remove barber because data is null");
            }
            var barber = await _barberRepository.GetByIdAsync(p => p.Id == request.Id);
            return await _barberRepository.RemoveAsync(barber);
        }
    }
}
