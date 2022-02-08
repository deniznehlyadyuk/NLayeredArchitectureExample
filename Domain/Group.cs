using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Group : BaseEntity
    {
        
        [ForeignKey("Student")]
        [Required]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
        
        
        [ForeignKey("Teacher")]
        [Required]
        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        
        
        [ForeignKey("Lesson")]
        [Required]
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        
        
  
        
        
        
    }
}