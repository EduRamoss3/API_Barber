using Barber.Application.CQRS.Barber.Commands;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Barber.Handlers
{
    public class UpdateBarberAsyncHandler : IRequestHandler<UpdateAsyncBarberCommand, bool>
    {
        private readonly IUnityOfWork _uof;
        public UpdateBarberAsyncHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<bool> Handle(UpdateAsyncBarberCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Erro na requisição");
            }
            var barber = await _uof.BarberRepository.GetByIdAsync(p => p.Id == request.Id);
            if(barber is null)
            {
                return false;
            }
            barber.Update(request.Disponibility, request.Name);
            _uof.BarberRepository.Update(barber);
            await _uof.Commit();
            return true;
        }
    }
}
