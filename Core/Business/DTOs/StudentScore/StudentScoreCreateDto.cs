namespace Core.Business.DTOs.StudentScore
{
    public class StudentScoreCreateDto : IDto
    {
        public string LessonName { get; set; }
        public float Score { get; set; }
        public int ExamNumber { get; set; }
    }
}