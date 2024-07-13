
namespace Barber.Domain.Validation
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string message) : base(message) { }
    }
}
