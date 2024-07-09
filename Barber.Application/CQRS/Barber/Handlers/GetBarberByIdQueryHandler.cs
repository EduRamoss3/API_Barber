using Barber.Application.CQRS.Barber.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Barber.Handlers
{
    public class GetBarberByIdQueryHandler : IRequestHandler<GetBarberByIdQuery, BarberMain>
    {
        private readonly IUnityOfWork _uof;
        public GetBarberByIdQueryHandler(IUnityOfWork uof)
        {
            _uof = uof;
           
        }
        public async Task<BarberMain> Handle(GetBarberByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var barber = await _uof.BarberRepository.GetByIdAsync(p => p.Id == request.Id);
         
            return barber;
        }
    }
}
