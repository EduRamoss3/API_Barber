using AutoMapper;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using MediatR;

namespace Barber.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ClientService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    
        public Task AddNewClient(ClientRegisterDTO clientDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetClients()
        {
            throw new NotImplementedException();
        }

        public Task RemoveClient(ClientDTO clientDTOt)
        {
            throw new NotImplementedException();
        }
    }
}
