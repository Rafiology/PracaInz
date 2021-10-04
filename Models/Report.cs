using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZgloszeniaIT.Models
{
    [Table("Report")]
    public class Report
    {
        [Key]
        public Guid IdReport { get; set; }
        [ForeignKey("ApplicationUser")]
        public Guid IdUser { get; set; }
        [ForeignKey("Status")]
        public Guid IdStatus { get; set; }
        [ForeignKey("Priority")]
        public Guid IdPriority { get; set; }
        [ForeignKey("Category")]
        public Guid IdCategory { get; set; }

        public Guid IdOperator { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime AddDate { get; set; }
        //[Required]
        [MaxLength(200)]
        public string Title { get; set; }
        //[Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(1000)]
        public string SolutionDesc { get; set; }
        public bool IsIncident { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Priority Priority { get; set; }
        public virtual Status Status { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}
