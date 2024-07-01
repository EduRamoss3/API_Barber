using Barber.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Barber.Application.DTOs
{
    public sealed class ClientDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(200,ErrorMessage ="Max 200 characters")]
        public string Name { get;set; }

        [Required(ErrorMessage = "Scheduled  is required!")]
        public bool Scheduled { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastTimeHere { get; set; }

        [JsonIgnore]
        public Schedules Schedule { get; set; }
    }
}
