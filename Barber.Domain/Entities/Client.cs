using Barber.Domain.Entities.Base;
using Barber.Domain.Validation;


namespace Barber.Domain.Entities
{
    public sealed class Client : Person
    {
        public int Points { get; private set; }
        public bool Scheduled { get; private set; } 
        public DateTime LastTimeHere { get; private set; }
        public string Email { get; private set; }

        public List<Schedules> Schedules { get; private set; } = new List<Schedules>();

        public Client(string name, int points, bool scheduled, DateTime lastTimeHere, string email) : base(name)
        {
            ValidateDomain(name, points, scheduled,lastTimeHere, email);
        }
        private void ValidateDomain(string name, int points,  bool scheduled, DateTime lastTimeHere, string email)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is Required!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Name is Required!");
            DomainExceptionValidation.When(name.Length > 250, "Name is too long");

            Points = points;
            Name = name;
            Scheduled = scheduled;
            LastTimeHere = lastTimeHere;
            Email = email;
        }
        public void SetSchedule(List<Schedules> schedule)
        {
            Schedules = schedule;
        }
        public void Update(string name,int points, bool scheduled, DateTime lastTimeHere, string email)
        {
            ValidateDomain(name, points, scheduled, lastTimeHere, email);
        }
        public void UpdatePoints()
        {
            Scheduled = true;
            if(Points == 0)
            {
                Points = 1;
            }
            if(Points > 0)
            {
                Points++;
            }
            
        }
        public void UpdateDate(DateTime date)
        {
            LastTimeHere = date;
        }
    }
}
