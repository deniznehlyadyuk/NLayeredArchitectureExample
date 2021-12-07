using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Domain
{
    public class HousekeeperResponsibleFloor : BaseEntity
    {
        [ForeignKey("Housekeeper")]
        [Required]
        public Guid HousekeeperId { get; set; }
        public virtual Housekeeper Housekeeper { get; set; }

        public int FloorNo { get; set; }
    }
}