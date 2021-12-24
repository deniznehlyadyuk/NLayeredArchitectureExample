using System;
using Core.Business.DTOs.Student;

namespace Core.Business.DTOs.Teacher
{
    public class TeacherGetDto : TeacherCreateDto ,IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}