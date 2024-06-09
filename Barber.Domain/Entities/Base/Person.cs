using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Entities.Base
{
    public abstract class Person
    {
        public int Id { get; protected set; }  
        public string Name { get; set; }


        public Person(string name)
        {
            Name = name;
        }
    }
}
