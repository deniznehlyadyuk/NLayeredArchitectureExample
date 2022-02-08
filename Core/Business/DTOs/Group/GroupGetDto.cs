using System;
using Core.Business.DTOs.Student;

namespace Core.Business.DTOs.Group
{
    public class GroupGetDto : IEntityGetDto
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string LessonName { get; set; }
        public string TeacherName { get; set; }
        
    }
}