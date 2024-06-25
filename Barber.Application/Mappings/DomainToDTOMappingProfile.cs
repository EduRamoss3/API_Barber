using AutoMapper;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Domain.Entities;

namespace Barber.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Barber.Domain.Entities.BarberMain, BarberRegisterDTO>().ReverseMap();
            CreateMap<Barber.Domain.Entities.BarberMain, BarberRegisterDTO>().ReverseMap();
            CreateMap<BarberMain, BarberDTO>().ReverseMap();
            CreateMap<Schedules, SchedulesDTO>().ReverseMap();  
            CreateMap<Client, ClientDTO>().ReverseMap();    
            CreateMap<Client,ClientRegisterDTO>().ReverseMap();
            
        }
    }
}
