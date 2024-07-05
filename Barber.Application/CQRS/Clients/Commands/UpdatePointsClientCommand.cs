
using MediatR;

namespace Barber.Application.CQRS.Clients.Commands
{
    public class UpdatePointsClientCommand : IRequest<bool>
    {
        public int Id { get; set; } 
        public UpdatePointsClientCommand(int id)
        {
            Id = id;
        }
    }
}
