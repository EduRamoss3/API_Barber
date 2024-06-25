using Barber.Domain.Entities;
using MediatR;

namespace Barber.Application.CQRS.Clients.Commands
{
    public class UpdateClientCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Scheduled { get; set; }
        public DateTime LastTimeHere { get; set; }
        public Schedules Schedule { get; set; }
        public int Points { get; set; }

        public UpdateClientCommand(int id)
        {
            Id = id;
        }   
    }
}
