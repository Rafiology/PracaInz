using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZgloszeniaIT.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public Guid IdDepartment { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
