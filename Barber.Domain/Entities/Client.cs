using Barber.Domain.Entities.Base;
using Barber.Domain.Validation;


namespace Barber.Domain.Entities
{
    public sealed class Client : Person
    {
        public int Points { get; private set; }
        public DateTime LastTimeHere { get; private set; }
        public string Email { get; private set; }

        public List<Schedules> Schedules { get; private set; } = new List<Schedules>();

        public Client(string name, int points, DateTime lastTimeHere, string email) : base(name)
        {
            ValidateDomain(name, points,lastTimeHere, email);
        }
        private void ValidateDomain(string name, int points, DateTime lastTimeHere, string email)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is Required!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Name is Required!");
            DomainExceptionValidation.When(name.Length > 250, "Name is too long");

            Points = points;
            Name = name;
            LastTimeHere = lastTimeHere;
            Email = email;
        }
        public void SetSchedule(List<Schedules> schedule)
        {
            Schedules = schedule;
        }
        public void Update(string name,int points,  DateTime lastTimeHere, string email)
        {
            ValidateDomain(name, points, lastTimeHere, email);
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
        public void UpdateDate(DateTime date)
        {
            LastTimeHere = date;
        }
    }
}
