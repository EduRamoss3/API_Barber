

using MediatR;

namespace Barber.Application.CQRS.Barber.Commands
{
    public class UpdateAsyncBarberCommand : IRequest<bool>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public bool Disponibility { get; set; }

        public UpdateAsyncBarberCommand(int id)
        {
            Id = id;
        }
    }
}
