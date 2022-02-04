using System;
using Core.Business.DTOs.Student;

namespace Core.Business.DTOs.Teacher
{
    public class TeacherGetDto : IEntityGetDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string LessonName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BornDate { get; set; }
    }
}