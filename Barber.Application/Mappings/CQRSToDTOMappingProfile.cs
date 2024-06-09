using AutoMapper;
using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;

namespace Barber.Application.Mappings
{
    public class CQRSToDTOMappingProfile : Profile
    {
        public CQRSToDTOMappingProfile()
        {
            CreateMap<BarberRegisterDTO, RegisterBarberCommand>().ReverseMap();
        }

    }
}
