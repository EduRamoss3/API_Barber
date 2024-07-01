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
        public DateTime DateSchedule { get; init; }

        [Required(ErrorMessage ="The value for service is required!")]
        public decimal ValueForService { get; init; }
        [Required(ErrorMessage ="Finalized is required")]
        public bool IsFinalized { get; init; }

        [JsonIgnore]
        public Client _Client { get; init; }

        [JsonIgnore]
        public Barber.Domain.Entities.BarberMain _Barber { get; init; }
    }
}
