using System;
using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Domain
{
    public class Person : BaseEntity
    {
        [MaxLength(128)]
        [Required]
        public string FullName { get; set; }
        
        [MaxLength(11)]
        [Required]
        public string IdentityNumber { get; set; }
        
        [Required]
        public DateTime BornDate { get; set; }
        
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}