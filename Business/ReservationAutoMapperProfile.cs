using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Doctor;
using Domain;

namespace Business
{
    public class ReservationAutoMapperProfile : Profile
    {
        public ReservationAutoMapperProfile()
        {
            CreateMap<AddressDto, Address>();
            CreateMap<DoctorCreateDto, Person>();
            CreateMap<DoctorUpdateDto, Person>();
            CreateMap<DoctorCreateDto, Doctor>();
            CreateMap<DoctorUpdateDto, Doctor>();
            CreateMap<Doctor, DoctorGetDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.Employee.Person.FullName))
                .ForMember(x => x.Phone, y => y.MapFrom(z => z.Employee.Person.Phone))
                .ForMember(x => x.IdentityNumber, y => y.MapFrom(z => z.Employee.Person.IdentityNumber))
                .ForMember(x => x.Address, y => y.MapFrom(z => new AddressDto
                {
                    Boulevard = z.Employee.Address.Boulevard,
                    District = z.Employee.Address.District,
                    Neighborhood = z.Employee.Address.Neighborhood,
                    Street = z.Employee.Address.Street,
                    BuildingNo = z.Employee.Address.BuildingNo,
                    RoomNo = z.Employee.Address.RoomNo
                }));

        }
    }
}