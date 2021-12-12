using System;

namespace Core.Business.DTOs.Doctor
{
    public class DoctorGetDto : DoctorCreateDto, IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}