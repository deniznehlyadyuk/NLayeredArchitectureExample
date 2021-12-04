using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Domain
{
    public class Lesson : BaseEntity
    {
        [MaxLength(48)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<StudentScore> StudentScores { get; set; }
    }
}