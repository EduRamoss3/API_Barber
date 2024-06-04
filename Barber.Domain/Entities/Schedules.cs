using Barber.Domain.Entities.Enums;
using Barber.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Entities
{
    public sealed class Schedules
    {
        public int Id { get; private set; }
        public int IdBarber { get; private set; }
        public int IdClient { get; private set; }
        public TypeOfService TypeOfService { get; private set; }    
        public DateTime DateSchedule { get; private set; }
        public decimal ValueForService { get; private set; }

        public Client _Client { get; private set; }  
        public Barber _Barber { get; private set; }
        
        private void ValidateDomain(int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueOfService)
        {
            DomainExceptionValidation.When(idBarber == 0, "ID Barber is required!");
            DomainExceptionValidation.When(idClient == 0, "ID Client is required!");
            DomainExceptionValidation.When(typeOfService == 0, "Error, type of service cant be null!");
            DomainExceptionValidation.When(string.Equals(dateSchedule.Year.ToString(), "0001"),"Error, Date Schedule is required!");
            DomainExceptionValidation.When(decimal.IsNegative(valueOfService),"Error!, Value of service has to be positive!");
            DomainExceptionValidation.When(valueOfService > 999,"Error, 999 Is maximum value!");
            IdBarber = idBarber;
            TypeOfService = typeOfService;
            DateSchedule = dateSchedule;
            ValueForService = valueOfService;
        }
        public Schedules(int id, int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueForService)
        {
            ValidateDomain(idBarber, idClient, typeOfService, dateSchedule, valueForService);
            Id = id;
        }
        public Schedules(int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueForService)
        {
            ValidateDomain(idBarber, idClient, typeOfService, dateSchedule, valueForService);
        }

        public void SetBarber(Barber barber)
        {
            _Barber = barber;
        }
        public void SetClient(Client client)
        {
            _Client = client;
        }
        public void UpdateValueForService(decimal amount)
        {
            ValueForService = amount;
        }
    }
}
