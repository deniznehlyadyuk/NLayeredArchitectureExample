using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Employee;

namespace Core.Business.DTOs.Doctor
{
    public class DoctorUpdateDto : IEmployeeEntityUpdateDto
    {
        public EmployeeUpdateDto EmployeeInfo { get; set; }
    }
}