using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Doctor;
using Core.Business.DTOs.Person;
using Domain;

namespace Business
{
    public class ReservationAutoMapperProfile : Profile
    {
        public ReservationAutoMapperProfile()
        {
            CreateMap<AddressDto, Address>();
            
            CreateMap<DoctorCreateDto, Person>()
                .ForMember(x=>x.Phone, y=>y.MapFrom(z=>z.PersonalInfo.Phone))
                .ForMember(x=>x.FullName, y=>y.MapFrom(z=>z.PersonalInfo.FullName))
                .ForMember(x=>x.IdentityNumber, y=>y.MapFrom(z=>z.PersonalInfo.IdentityNumber));
            CreateMap<DoctorUpdateDto, Person>()
                .ForMember(x=>x.Phone, y=>y.MapFrom(z=>z.PersonalInfo.Phone))
                .ForMember(x=>x.FullName, y=>y.MapFrom(z=>z.PersonalInfo.FullName));
            CreateMap<DoctorCreateDto, Doctor>();
            CreateMap<DoctorUpdateDto, Doctor>();
            CreateMap<DoctorCreateDto, Employee>();
            CreateMap<DoctorUpdateDto, Employee>();
            CreateMap<Doctor, DoctorGetDto>()
                .ForMember(x => x.PersonalInfo, y => y.MapFrom(z => new PersonCreateDto
                {
                    Phone = z.Employee.Person.Phone,
                    FullName = z.Employee.Person.FullName,
                    IdentityNumber = z.Employee.Person.IdentityNumber
                }))
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