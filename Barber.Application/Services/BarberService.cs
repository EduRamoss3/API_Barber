using AutoMapper;
using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.CQRS.Barber.Queries;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task AddNewBarberAsync(BarberRegisterDTO barberDTO)
        {
            var registerBarberCommand = _mapper.Map<RegisterBarberCommand>(barberDTO);
            await _mediator.Send(registerBarberCommand);
        }

        public async Task<IEnumerable<BarberDTO>> GetBarbersAsync()
        {
            GetBarbersQuery getBarbersQuery = new GetBarbersQuery();
            var barbersEntity = await _mediator.Send(getBarbersQuery);
            return _mapper.Map<IEnumerable<BarberDTO>>(barbersEntity);
        }

        public async Task RemoveBarberByIdAsync(int id)
        {
            var removeBarberCommand = new RemoveBarberCommand(id);
            await _mediator.Send(removeBarberCommand);
        }

        public async Task SetDisponibilityAsync(int id, bool disponibility)
        {
            UpdateBarberCommand updateBarberCommand = new UpdateBarberCommand(id);
            await _mediator.Send(updateBarberCommand);
        }

        public async Task<BarberDTO> GetBarberByIdAsync(int id)
        {
            GetBarberByIdQuery getBarberByIdQuery = new GetBarberByIdQuery(id);
            var barbersEntity = await _mediator.Send(getBarberByIdQuery);
            return _mapper.Map<BarberDTO>(barbersEntity);
        }
    }
}
