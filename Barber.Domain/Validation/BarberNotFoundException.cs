

namespace Barber.Domain.Validation
{
    public class BarberNotFoundException : Exception
    {
        public BarberNotFoundException(string message) : base(message) { }  
    }
}
