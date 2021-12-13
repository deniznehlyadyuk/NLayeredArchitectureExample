using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Doctor;
using Core.Business.DTOs.Employee;
using Core.Business.DTOs.Person;
using Core.Business.DTOs.Secretary;
using Core.Business.DTOs.SecretaryDoctor;
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

            CreateMap<SecretaryDoctorCreateDto, SecretaryDoctor>();
            CreateMap<SecretaryDoctor, SecretaryDoctorGetDto>()
                .ForMember(x=>x.DoctorInfo, y=>y.MapFrom(x=>new DoctorGetDto
                {
                    Id = x.DoctorId,
                    EmployeeInfo = new EmployeeCreateDto
                    {
                        PersonalInfo = new PersonCreateDto
                        {
                            Phone = x.Doctor.Employee.Person.Phone,
                            FullName = x.Doctor.Employee.Person.FullName   
                        }
                    }
                }))
                .ForMember(x=>x.SecretaryInfo, y=>y.MapFrom(x=>new SecretaryGetDto
                {
                    Id = x.SecretaryId,
                    EmployeeInfo = new EmployeeCreateDto
                    {
                        PersonalInfo = new PersonCreateDto
                        {
                            Phone = x.Secretary.Employee.Person.Phone,
                            FullName = x.Secretary.Employee.Person.FullName
                        } 
                    },
                    LandPhoneCode = x.Secretary.LandPhoneCode
                }));
        }
    }
}