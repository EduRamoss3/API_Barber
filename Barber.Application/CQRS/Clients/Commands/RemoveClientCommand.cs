using MediatR;

namespace Barber.Application.CQRS.Clients.Commands
{
    public class RemoveClientCommand : IRequest<bool>
    {
        public int Id { get; set; } 
        public RemoveClientCommand(int id)
        {
            Id = id;
        }   
    }
}
