using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Person;

namespace Core.Business.DTOs.Employee
{
    public class EmployeeUpdateDto : IDto
    {
        public PersonUpdateDto PersonalInfo { get; set; }
        public AddressDto Address { get; set; }
    }
}