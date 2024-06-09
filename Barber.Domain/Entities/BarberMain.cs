using Barber.Domain.Entities.Base;
using Barber.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Barber.Domain.Entities
{
    public sealed class BarberMain : Person
    {
        public bool Disponibility { get; private set; }
        public List<Schedules> Schedules { get; private set; } = new List<Schedules>(); 

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is Required!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Name is Required!");
            DomainExceptionValidation.When(name.Length > 250, "Name is too long");
            Name = name;

        }
        public BarberMain(string name, bool disponibility) :base(name)
        {
            ValidateDomain(name);
            Disponibility = disponibility;
        }
        public void AddSchedules(Schedules schedule)
        {
            Schedules.Add(schedule);
        }
        public void SetDisponibility(bool disponibility)
        {
            Disponibility = disponibility;
        }
        public void Update(bool disponibility, string name)
        {
            Disponibility = disponibility;
            Name = name;        
        }

    }
}
