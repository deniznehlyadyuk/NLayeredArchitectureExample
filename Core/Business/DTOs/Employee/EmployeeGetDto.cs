using System;
using Core.Business.DTOs.Abstract;

namespace Core.Business.DTOs.Employee
{
    public class EmployeeGetDto : EmployeeCreateDto, IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}