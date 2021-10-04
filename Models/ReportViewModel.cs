using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZgloszeniaIT.Models
{
    public class ReportViewModel
    {
        public Guid IdReport { get; set; }
        public string User { get; set; }
        public string Operator { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public System.DateTime AddDate { get; set; }
        [Required(ErrorMessage = "Wprowadź tytuł zgłoszenia!", AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Wprowadź treść zgłoszenia!", AllowEmptyStrings = false)]
        public string Description { get; set; }
        public string SolutionDesc { get; set; }
        public List<Comment> CommentList { get; set; }
        public List<Attachment> AttachmentList { get; set; }
        public List<Priority> PriorityList { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Status> StatusList { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
    }
}
