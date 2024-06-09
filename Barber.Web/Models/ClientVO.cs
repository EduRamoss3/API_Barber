namespace Barber.Web.Models
{
    public class ClientVO
    {
        public int Id { get; set; }
        public bool Scheduled { get; set; }
        public DateTime LastTimeHere { get; set; }
        public SchedulesVO Schedule { get; set; }
    }
}
