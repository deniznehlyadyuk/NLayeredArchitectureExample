using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Employee;

namespace Core.Business.DTOs.Secretary
{
    public class SecretaryCreateDto : IEmployeeEntityCreateDto
    {
        public EmployeeCreateDto EmployeeInfo { get; set; }
        public int LandPhoneCode { get; set; }
    }
}