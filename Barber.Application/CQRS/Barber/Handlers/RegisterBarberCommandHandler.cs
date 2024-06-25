using Barber.Application.CQRS.Barber.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Barber.Handlers
{
    public class RegisterBarberCommandHandler : IRequestHandler<RegisterBarberCommand, BarberMain>
    {
        private readonly IBarberRepository _barberRepository;
        public RegisterBarberCommandHandler(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }
        public async Task<BarberMain> Handle(RegisterBarberCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error adding new barber, verify all data and try again");
            }
            BarberMain barberMain = new BarberMain(request.Name, request.Disponibility);
            return await _barberRepository.AddNewBarberAsync(barberMain);
        }
    }

}