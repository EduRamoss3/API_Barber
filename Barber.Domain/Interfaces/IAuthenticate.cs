using Barber.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Interfaces
{
    public interface IAuthenticate
    {
        Task<Validator> Authenticate(string email, string password);
        Task<Validator> RegisterUser(string email, string password);
        Task Logout();
    }
}
