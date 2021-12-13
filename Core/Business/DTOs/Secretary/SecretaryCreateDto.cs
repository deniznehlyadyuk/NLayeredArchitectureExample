using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Secretary
{
    public class SecretaryCreateDto : IDto
    {
        public PersonCreateDto PersonalInfo { get; set; }
        public AddressDto Address { get; set; }
    }
}