using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Doctor;
using Core.Business.DTOs.Employee;
using Core.Business.DTOs.Person;
using Core.Business.DTOs.Secretary;
using Domain;

namespace Business
{
    public class ReservationAutoMapperProfile : Profile
    {
        public ReservationAutoMapperProfile()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
            
            CreateMap<PersonCreateDto, Person>().ReverseMap();
            CreateMap<PersonUpdateDto, Person>();
            
            CreateMap<Employee, EmployeeGetDto>()
                .ForMember(x => x.Address, y => y.MapFrom(x => x.Address))
                .ForMember(x => x.PersonalInfo, y => y.MapFrom(x => x.Person));
            CreateMap<EmployeeCreateDto, Employee>();

            CreateMap<DoctorCreateDto, Doctor>();
            CreateMap<DoctorUpdateDto, Doctor>();
            CreateMap<Doctor, DoctorGetDto>();

            CreateMap<SecretaryCreateDto, Secretary>();
            CreateMap<SecretaryUpdateDto, Secretary>();
            CreateMap<Secretary, SecretaryGetDto>();
        }
    }
}