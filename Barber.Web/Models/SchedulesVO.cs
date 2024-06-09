using Barber.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Barber.Web.Models
{
    public class SchedulesVO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ID Barber is required!")]
        public int IdBarber { get; set; }
        [Required(ErrorMessage = "ID Client is required!")]
        public int IdClient { get; set; }
        [Required(ErrorMessage = "Type of service is required!")]
        public TypeOfService TypeOfService { get; set; }
        [Required(ErrorMessage = "Date to schedule is required!")]
        public DateTime DateSchedule { get; set; }
        [Required(ErrorMessage = "The value for service is required!")]
        public decimal ValueForService { get; set; }

        public ClientVO _Client { get; set; }
        public BarberVO _Barber { get; set; }
    }
}
