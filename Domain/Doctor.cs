using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Doctor : BaseEntity
    {
        [ForeignKey("Employee")]
        [Required]
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual SecretaryDoctor SecretaryDoctors { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}