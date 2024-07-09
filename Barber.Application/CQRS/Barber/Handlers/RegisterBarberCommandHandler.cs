using Barber.Application.CQRS.Barber.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Barber.Handlers
{
    public class RegisterBarberCommandHandler : IRequestHandler<RegisterBarberCommand, bool>
    {
        private readonly IUnityOfWork _uof;
        public RegisterBarberCommandHandler(IUnityOfWork uof){
            _uof = uof;
        }
        public async Task<bool> Handle(RegisterBarberCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error adding new barber, verify all data and try again");
            }
            BarberMain barberMain = new BarberMain(request.Name, request.Disponibility);
            var barber = await _uof.BarberRepository.AddAsync(barberMain);
            await _uof.Commit();
            return barber;
        }
    }

}