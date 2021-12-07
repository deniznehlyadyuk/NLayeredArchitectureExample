using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Domain
{
    public class Address : BaseEntity
    {
        [Required]
        public string District { get; set; }
        
        [Required]
        public string Neighborhood { get; set; }
        
        public string Boulevard { get; set; }
        public string Street { get; set; }
        
        [Required]
        public int BuildingNo { get; set; }
        
        [Required]
        public int RoomNo { get; set; }

        public virtual Employee Employee { get; set; }
    }
}