using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.DTOs;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Barber.Handlers
{
    public class UpdateBarberCommandHandler : IRequestHandler<UpdateBarberCommand, BarberMain>
    {
        private readonly IBarberRepository _barberRepository;
        public UpdateBarberCommandHandler(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public async Task<BarberMain> Handle(UpdateBarberCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error, barber cannot be null");
            }
            var barber = await _barberRepository.GetBarberByIdAsync(request.Id);
            if (barber is null)
            {
                throw new ApplicationException("Error, barber no exist");
            }
            barber.SetDisponibility(request.Disponibility);
            return await _barberRepository.UpdateAsync(barber); 
        }
    }
}
