using System;
using System.ComponentModel.DataAnnotations;

namespace ZgloszeniaIT.Models
{
    public class KnowledgeBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime AddDate { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
