using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Reservation : BaseEntity
    {
        [ForeignKey("Patient")]
        [Required]
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        [Required]
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        [Required]
        public DateTime ResDate { get; set; }
        
        [Required]
        public bool IsCanceled { get; set; }
    }
}