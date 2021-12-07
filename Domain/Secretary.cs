using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Secretary : BaseEntity
    {
        [ForeignKey("Employee")]
        [Required]
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        public int LandPhoneCode { get; set; }
    }
}