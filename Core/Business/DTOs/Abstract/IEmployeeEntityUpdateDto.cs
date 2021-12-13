using Core.Business.DTOs.Employee;

namespace Core.Business.DTOs.Abstract
{
    public interface IEmployeeEntityUpdateDto : IDto
    {
        public EmployeeUpdateDto EmployeeInfo { get; set; }
    }
}