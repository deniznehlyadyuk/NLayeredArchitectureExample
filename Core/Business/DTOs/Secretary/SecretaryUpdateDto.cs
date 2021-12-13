using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Employee;

namespace Core.Business.DTOs.Secretary
{
    public class SecretaryUpdateDto : IEmployeeEntityUpdateDto
    {
        public EmployeeUpdateDto EmployeeInfo { get; set; }
        public int LandPhoneCode { get; set; }
    }
}