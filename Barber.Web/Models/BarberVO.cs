namespace Barber.Web.Models
{
    public class BarberVO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Disponibility { get; set; }
        public List<SchedulesVO> Schedules { get; set; } = new List<SchedulesVO>();
    }
}
