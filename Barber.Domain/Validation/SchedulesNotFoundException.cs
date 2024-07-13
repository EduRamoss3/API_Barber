
namespace Barber.Domain.Validation
{
    public class SchedulesNotFoundException : Exception
    {
        public SchedulesNotFoundException(string message) :base(message) { }
    }
}
