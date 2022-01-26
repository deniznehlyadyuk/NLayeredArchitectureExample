using System;

namespace Core.Business.DTOs.StudentScore
{
    public class StudentScoreUpdateDto : IDto
    {
        public Guid LessonId { get; set; }
        public Guid StudentId { get; set; }
        public float Score { get; set; }
    }
}