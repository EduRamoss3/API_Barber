using Barber.Domain.Entities.Enums;
using Barber.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Barber.Application.DTOs
{
    public sealed record SchedulesDTO
    {
        [Key]
        public int Id { get; init; }

        [Required(ErrorMessage ="ID Barber is required!")]
        public int IdBarber { get; init; }

        [Required(ErrorMessage = "ID Client is required!")]
        public int IdClient { get; init; }

        [Required(ErrorMessage ="Type of service is required!")]
        public TypeOfService TypeOfService { get; init; }

        [Required(ErrorMessage ="Date to schedule is required!")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateSchedule { get; init; }

        [Required(ErrorMessage ="The value for service is required!")]
        public decimal ValueForService { get; init; }
        [Required(ErrorMessage ="Finalized is required")]
        public bool IsFinalized { get; init; }

        public string ClientName { get; init; }

        public string BarberName { get; init; }
    }
}
