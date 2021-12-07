using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class SecretaryDoctor : BaseEntity
    {
        [ForeignKey("Secretary")]
        [Required]
        public Guid SecretaryId { get; set; }
        public virtual Secretary Secretary { get; set; }

        [ForeignKey("Doctor")]
        [Required]
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}