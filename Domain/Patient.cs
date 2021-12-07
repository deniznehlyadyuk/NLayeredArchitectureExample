using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Patient : BaseEntity
    {
        [ForeignKey("Person")]
        [Required]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<PhysicalExamination> PhysicalExaminations { get; set; }
    }
}