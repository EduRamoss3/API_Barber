using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Interfaces
{
    public interface ISeedRolesInitial
    {
       public void SeedRoles();
       public Task SeedUsers();
    }
}
