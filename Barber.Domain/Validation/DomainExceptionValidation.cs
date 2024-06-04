using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string message) : base(message)
        {
        }
        public static void When(bool condition, string error)
        {
            if (condition)
            {
                throw new DomainExceptionValidation(error);
            }
        }
    }
}
