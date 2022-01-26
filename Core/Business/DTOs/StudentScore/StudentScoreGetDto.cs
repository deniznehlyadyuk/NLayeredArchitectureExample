using System;
using Core.Business.DTOs.Student;

namespace Core.Business.DTOs.StudentScore
{
    public class StudentScoreGetDto : IEntityGetDto
    {
        public string StudentName { get; set; }
        public string LessonName { get; set; }
        public float Score { get; set; }
        public int ExamNumber { get; set; }
        public Guid Id { get; set; }
    }
}