using System;

namespace Core.Business.DTOs.Teacher
{
    public class TeacherUpdateDto : IDto
    {
        public string FullName { get; set; }
        public DateTime BornDate { get; set; }
    }
}