using Barber.Application.CQRS.Clients.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;


namespace Barber.Application.CQRS.Clients.Handlers
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
    {
        private readonly IUnityOfWork _uof;

        public GetClientByIdQueryHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var client = await _uof.ClientRepository.GetByIdAsync(p => p.Id == request.Id);
            if(client is not Client)
            {
                throw new ApplicationException("Data format error");
            }
            return client;
        }
    }
}
