using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Teacher : BaseEntity
    {
        [ForeignKey("Person")]
        [Required]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
        [ForeignKey("Lesson")]
        [Required]
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<StudentLesson> StudentLessons { get; set; }
    }
}