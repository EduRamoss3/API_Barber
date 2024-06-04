using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Entities.Enums
{
    public enum TypeOfService : int
    {
        Normal = 1,
        Gradient = 2,
        GradientAndBeard = 3,
        Beard = 4,  
        Coloring = 5,
        ColoringOnlyBeardAndNormal = 6,
        ColoringAllAndGradient = 7,
        ColoringAllAndNormal = 8,
        ColoringOnlyBeardAndGradient = 9,
        OnlyWash = 10,
        Wax = 11,
    }
}
