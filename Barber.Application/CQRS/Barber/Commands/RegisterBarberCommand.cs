
using MediatR;

namespace Barber.Application.CQRS.Barber.Commands
{
    public class RegisterBarberCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public bool Disponibility { get; set; }
    }
}
