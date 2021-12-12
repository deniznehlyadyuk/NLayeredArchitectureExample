using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Doctor
{
    public class DoctorUpdateDto : IDto
    {
        public PersonUpdateDto PersonalInfo { get; set; }
        public AddressDto Address { get; set; }
    }
}