using Barber.Domain.Entities.Base;
using Barber.Domain.Validation;


namespace Barber.Domain.Entities
{
    public sealed class Client : Person
    {
        public int Points { get; private set; }
        public bool Scheduled { get; private set; } 
        public DateTime LastTimeHere { get; private set; }

        public List<Schedules> Schedules { get; private set; } = new List<Schedules>();

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
        public void SetSchedule(List<Schedules> schedule)
        {
            Schedules = schedule;
        }
        public void Update(string name,int points, bool scheduled, DateTime lastTimeHere)
        {
            ValidateDomain(name, points, scheduled, lastTimeHere);
        }
        public void UpdatePoints()
        {
            if(Points == 0)
            {
                Points = 1;
            }
            if(Points > 0)
            {
                Points++;
            }
            
        }
    }
}
