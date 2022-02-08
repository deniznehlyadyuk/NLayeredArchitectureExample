using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Student : BaseEntity
    {
        [ForeignKey("Person")]
        [Required]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        [MaxLength(6)]
        [Required]
        public string SchoolNumber { get; set; }
        
        public virtual ICollection<StudentScore> StudentScores { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}