using Core.Business.DTOs.Employee;

namespace Core.Business.DTOs.Abstract
{
    public interface IEmployeeEntityCreateDto : IDto
    {
        public EmployeeCreateDto EmployeeInfo { get; set; }
    }
}