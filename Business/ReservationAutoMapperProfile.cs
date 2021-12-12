using AutoMapper;
using Core.Business.DTOs;
using Core.Business.DTOs.Doctor;
using Core.Business.DTOs.Person;
using Core.Business.DTOs.Reservation;
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

            CreateMap<ReservationCreateDto, Person>()
                .ForMember(x => x.Phone, y => y.MapFrom(z => z.PatientInfo.Phone))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.PatientInfo.FullName))
                .ForMember(x => x.IdentityNumber, y => y.MapFrom(z => z.PatientInfo.IdentityNumber));
            CreateMap<ReservationUpdateDto, Person>()
                .ForMember(x => x.Phone, y => y.MapFrom(z => z.PatientInfo.Phone))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.PatientInfo.FullName));
            CreateMap<ReservationCreateDto, Reservation>();
            CreateMap<ReservationUpdateDto, Reservation>();
            CreateMap<Reservation, ReservationGetDto>()
                .ForMember(x => x.DoctorInfo, y => y.MapFrom(z => new PersonCreateDto
                {
                    Phone = z.Doctor.Employee.Person.Phone,
                    FullName = z.Doctor.Employee.Person.FullName,
                    IdentityNumber = z.Doctor.Employee.Person.IdentityNumber
                }))
                .ForMember(x=>x.PatientInfo, y=>y.MapFrom(z=>new PersonCreateDto
                {
                    Phone = z.Patient.Person.Phone,
                    FullName = z.Patient.Person.FullName,
                    IdentityNumber = z.Patient.Person.IdentityNumber
                }));
        }
    }
}