using Barber.Application.CQRS.Barber.Commands;
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
    public class RemoveBarberCommandHandler : IRequestHandler<RemoveBarberCommand, BarberMain>
    {
        private readonly IBarberRepository _barberRepository;
        public RemoveBarberCommandHandler(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public async Task<BarberMain> Handle(RemoveBarberCommand request, CancellationToken cancellationToken)
        {
            var barber = await _barberRepository.GetBarberByIdAsync(request.Id);
            if(request is null)
            {
                throw new ApplicationException("Error, cannot remove barber because data is null");
            }

            return await _barberRepository.RemoveBarberAsync(barber);
        }
    }
}
