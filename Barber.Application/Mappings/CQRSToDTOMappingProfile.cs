using AutoMapper;
using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.CQRS.Clients.Commands;
using Barber.Application.CQRS.Schedule.Commands;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;

namespace Barber.Application.Mappings
{
    public class CQRSToDTOMappingProfile : Profile
    {
        public CQRSToDTOMappingProfile()
        {
            CreateMap<BarberRegisterDTO, RegisterBarberCommand>().ReverseMap();
            CreateMap<BarberDTO, UpdateBarberCommand>().ReverseMap();   
            CreateMap<BarberDTO, RemoveBarberCommand>().ReverseMap();
            CreateMap<BarberUpdateDTO, UpdateAsyncBarberCommand>().ReverseMap(); 

            CreateMap<SchedulesDTO, UpdateScheduleCommand>().ReverseMap();
            CreateMap<SchedulesDTO, UpdateValueForScheduleCommand>().ReverseMap();
            CreateMap<SchedulesDTO, RemoveScheduleCommand>().ReverseMap();
            CreateMap<SchedulesDTO, AddScheduleCommand>().ReverseMap();

            CreateMap<ClientDTO, RegisterClientCommand>().ReverseMap();
            CreateMap<ClientDTO, RemoveClientCommand>().ReverseMap();
            CreateMap<ClientDTO, UpdateClientCommand>().ReverseMap();
            CreateMap<ClientRegisterDTO, RegisterClientCommand>().ReverseMap();

        }

    }
}
