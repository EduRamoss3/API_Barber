using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Validation
{
    public class Validator
    {
        public List<string> Message { get; set; } = new List<string>();
        public bool IsSucceded { get; set; }
    }
}
