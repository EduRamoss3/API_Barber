using Barber.Application.CQRS.Clients.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Clients.Handlers
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<Client>>
    {
        private readonly IClientRepository _clientRepository;
        public GetClientsQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }   

        public async Task<IEnumerable<Client>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            return await _clientRepository.GetAllAsync();
        }
    }
}
