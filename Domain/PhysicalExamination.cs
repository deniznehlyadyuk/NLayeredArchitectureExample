using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class PhysicalExamination : BaseEntity
    {
        [ForeignKey("Doctor")]
        [Required]
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [Required]
        public string Diagnosis { get; set; }
    }
}