using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Employee
{
    public class EmployeeCreateDto : IDto
    {
        public PersonCreateDto PersonalInfo { get; set; }
        public AddressDto Address { get; set; }
    }
}