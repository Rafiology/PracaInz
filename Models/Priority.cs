using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZgloszeniaIT.Models
{
    [Table("Priority")]
    public class Priority
    {
        [Key]
        public Guid IdPriority { get; set; }
        [MaxLength(50)]
        public string PriorityDesc { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
