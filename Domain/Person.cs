using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace Domain
{
    public class Person : BaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string FullName { get; set; }
        
        [Required]
        [MaxLength(10)] //örnek: 5531173456
        public string Phone { get; set; }

        public virtual Employee Employee { get; set; }
    }
}