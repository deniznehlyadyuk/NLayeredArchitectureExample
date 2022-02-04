using System;

namespace Core.Business.DTOs.Teacher
{
    public class TeacherCreateDto : IDto
    {
        public string FullName { get; set; }

        public Guid LessonId { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BornDate { get; set; }
    }
}