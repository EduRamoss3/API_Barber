using AutoMapper;
using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.CQRS.Barber.Queries;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using MediatR;

namespace Barber.Application.Services
{
    public class BarberService : IBarberService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BarberService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<bool>AddAsync(BarberRegisterDTO barberRegisterDTO)
        {
            var registerBarberCommand = _mapper.Map<RegisterBarberCommand>(barberRegisterDTO);
            var entity = await _mediator.Send(registerBarberCommand);
            return entity is not null ? true : false;
        }

        public async Task<IEnumerable<BarberDTO>> GetAllAsync()
        {
            GetBarbersQuery getBarbersQuery = new GetBarbersQuery();
            var barbersEntity = await _mediator.Send(getBarbersQuery);
            return _mapper.Map<IEnumerable<BarberDTO>>(barbersEntity);
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var removeBarberCommand = new RemoveBarberCommand(id);
            return await _mediator.Send(removeBarberCommand);
        }

        public async Task<bool> SetDisponibilityAsync(int id, bool disponibility)
        {
            UpdateBarberCommand updateBarberCommand = new UpdateBarberCommand(id, disponibility);
            return await _mediator.Send(updateBarberCommand);
        }

        public async Task<BarberDTO> GetByIdAsync(int id)
        {
            GetBarberByIdQuery getBarberByIdQuery = new GetBarberByIdQuery(id);
            var barbersEntity = await _mediator.Send(getBarberByIdQuery);
            return _mapper.Map<BarberDTO>(barbersEntity);
        }
    }
}
