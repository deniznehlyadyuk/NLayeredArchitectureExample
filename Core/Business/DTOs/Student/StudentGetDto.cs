using System;

namespace Core.Business.DTOs.Student
{
    public class StudentGetDto : StudentDto, IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}