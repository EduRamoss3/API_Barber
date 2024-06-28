using Barber.Domain.Entities.Enums;
using Barber.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Barber.Application.DTOs
{
    public sealed class SchedulesDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="ID Barber is required!")]
        public int IdBarber { get; set; }
        [Required(ErrorMessage = "ID Client is required!")]
        public int IdClient { get; set; }
        [Required(ErrorMessage ="Type of service is required!")]
        public TypeOfService TypeOfService { get; set; }
        [Required(ErrorMessage ="Date to schedule is required!")]
        public DateTime DateSchedule { get; set; }
        [Required(ErrorMessage ="The value for service is required!")]
        public decimal ValueForService { get; set; }
        [JsonIgnore]
        public Client _Client { get; set; }
        [JsonIgnore]
        public Barber.Domain.Entities.BarberMain _Barber { get; set; }
    }
}
