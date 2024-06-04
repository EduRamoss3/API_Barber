using Barber.Domain.Entities.Base;
using Barber.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Entities
{
    public sealed class Client : Person
    {
        public int Points { get; private set; }
        public bool Scheduled { get; private set; } 
        public DateTime LastTimeHere { get; private set; }  

        public Schedules Schedule { get; private set; }

        public Client(string name, int points, bool scheduled, DateTime lastTimeHere) : base(name)
        {
            ValidateDomain(name, points, scheduled,lastTimeHere);
        }
        private void ValidateDomain(string name, int points,  bool scheduled, DateTime lastTimeHere)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is Required!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Name is Required!");
            DomainExceptionValidation.When(name.Length > 250, "Name is too long");

            Points = points;
            Name = name;
            Scheduled = scheduled;
            LastTimeHere = lastTimeHere;
        }
        public void SetSchedule(Schedules schedule)
        {
            Schedule = schedule;
        }
    }
}
