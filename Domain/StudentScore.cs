using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class StudentScore : BaseEntity
    {
        [ForeignKey("Student")]
        [Required]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Lesson")]
        [Required]
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }

        [Required]
        public int ExamNumber { get; set; }
        
        [Required]
        public float Score { get; set; }
    }
}