using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.DTOs;
using Barber.Domain.Entities;
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
        public Task<BarberMain> Handle(UpdateBarberCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
