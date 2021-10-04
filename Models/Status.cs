using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZgloszeniaIT.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]
        public Guid IdStatus { get; set; }

        [MaxLength(50)]
        public string StatusDesc { get; set; }

        public virtual ICollection<Report> Report { get; set; }

    }
}
