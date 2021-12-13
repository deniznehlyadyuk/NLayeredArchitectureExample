using System;
using Core.Business.DTOs.Abstract;

namespace Core.Business.DTOs.Doctor
{
    public class DoctorGetDto : DoctorCreateDto, IEmployeeEntityGetDto
    {
        public Guid Id { get; set; }
    }
}