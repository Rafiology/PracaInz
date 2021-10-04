using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZgloszeniaIT.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public Guid IdComment { get; set; }
        public Guid AuthorId { get; set; }
        [NotMapped]
        public string Author { get; set; }
        [ForeignKey("Report")]
        public Guid IdReport { get; set; }

        [MaxLength(400)]
        public string ComContent { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime AddDate { get; set; }

        public virtual Report Report { get; set; }

    }
}
