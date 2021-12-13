using Core.Business.DTOs.Abstract;
using Core.Business.DTOs.Employee;

namespace Core.Business.DTOs.Doctor
{
    public class DoctorCreateDto : IEmployeeEntityCreateDto
    {
        public EmployeeCreateDto EmployeeInfo { get; set; }
    }
}