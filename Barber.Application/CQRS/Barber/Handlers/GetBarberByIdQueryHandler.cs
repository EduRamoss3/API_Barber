using Barber.Application.CQRS.Barber.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Barber.Handlers
{
    public class GetBarberByIdQueryHandler : IRequestHandler<GetBarberByIdQuery, BarberMain>
    {
        private readonly IBarberRepository _barberRepository;
        public GetBarberByIdQueryHandler(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
           
        }
        public async Task<BarberMain> Handle(GetBarberByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var barber = await _barberRepository.GetBarberByIdAsync(request.Id);
         
            return barber;
        }
    }
}
