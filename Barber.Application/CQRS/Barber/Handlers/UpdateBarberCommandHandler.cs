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
                throw new ApplicationException("Error in request");
            }
            var barber = await _uof.BarberRepository.GetByIdAsync(p => p.Id == request.Id);
            if (barber is null)
            {
                return false;
                
            }
            barber.SetDisponibility(request.Disponibility);
            var result =  _uof.BarberRepository.SetDisponibility(barber, request.Disponibility);
            await _uof.Commit();
            return result;
        }
    }
}
