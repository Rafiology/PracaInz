using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZgloszeniaIT.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid IdCategory { get; set; }
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public virtual ICollection<Report> Report { get; set; }
    }
}
