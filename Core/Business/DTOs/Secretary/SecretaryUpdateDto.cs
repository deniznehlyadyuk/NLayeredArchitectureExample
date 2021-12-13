using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Secretary
{
    public class SecretaryUpdateDto : IDto
    {
        public PersonUpdateDto PersonalInfo { get; set; }
        public AddressDto Address { get; set; }
    }
}