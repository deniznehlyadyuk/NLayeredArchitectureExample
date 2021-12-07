using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class Employee : BaseEntity
    {
        [ForeignKey("Person")]
        [Required]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        [ForeignKey("Address")]
        [Required]
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Housekeeper Housekeeper { get; set; }
        public virtual Secretary Secretary { get; set; }
        public virtual EmployeeSalary EmployeeSalary { get; set; }
        public virtual ICollection<EmployeePermits> EmployeePermits { get; set; }
    }
}