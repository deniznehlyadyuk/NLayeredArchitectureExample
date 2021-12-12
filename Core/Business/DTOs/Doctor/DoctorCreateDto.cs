using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Doctor
{
    public class DoctorCreateDto : IDto
    {
        public PersonCreateDto PersonalInfo { get; set; }
        public AddressDto Address { get; set; }
    }
}