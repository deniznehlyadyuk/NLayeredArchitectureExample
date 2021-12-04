using System;
using Core.Business.DTOs.Student;

namespace Core.Business.DTOs.Lesson
{
    public class LessonGetDto : LessonDto, IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}