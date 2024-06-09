using Barber.Application.DTOs;
using Barber.Domain.Entities;
using MediatR;


namespace Barber.Application.CQRS.Barber.Commands
{
    public class BarberCommand : IRequest<BarberMain>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public bool Disponibility { get; set; }
        public List<SchedulesDTO> Schedules { get;set; } = new List<SchedulesDTO>();
    }
}
