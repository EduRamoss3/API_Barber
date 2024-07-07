using Barber.Domain.Entities.Enums;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Commands
{
    public class UpdateScheduleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int IdBarber { get; private set; }
        public int IdClient { get; private set; }
        public TypeOfService TypeOfService { get; private set; }
        public DateTime DateSchedule { get; private set; }
        public decimal ValueForService { get; set; }
        public bool IsFinalized { get; set; }
        public UpdateScheduleCommand(int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueForService, bool isFinalized)
        {
            IdBarber = idBarber;
            IdClient = idClient;
            TypeOfService = typeOfService;
            DateSchedule = dateSchedule;
            ValueForService = valueForService;
            IsFinalized = isFinalized;
        }
    }
}
