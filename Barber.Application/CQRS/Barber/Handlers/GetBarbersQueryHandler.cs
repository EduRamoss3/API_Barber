using Barber.Application.CQRS.Barber.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Barber.Handlers
{
    public class GetBarbersQueryHandler : IRequestHandler<GetBarbersQuery, IEnumerable<BarberMain>>
    {
        private readonly IBarberRepository _barberRepository;
        public GetBarbersQueryHandler(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;

        }
        public async Task<IEnumerable<BarberMain>> Handle(GetBarbersQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var barbers = await _barberRepository.GetAllAsync();
            return barbers;
        }
    }
}
