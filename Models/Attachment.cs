using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZgloszeniaIT.Models
{
    [Table("Attachment")]
    public class Attachment
    {
        [Key]
        public Guid IdAttachment { get; set; }
        [ForeignKey("Report")]
        public Guid IdReport { get; set; }
        
        [MaxLength(500)]
        public string AttachmentPath { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime AddDate { get; set; }

        public virtual Report Report { get; set; }
    }
}
