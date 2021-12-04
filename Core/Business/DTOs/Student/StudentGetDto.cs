using System;

namespace Core.Business.DTOs.Student
{
    public class StudentGetDto : StudentCreateDto, IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}