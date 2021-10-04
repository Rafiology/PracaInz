using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ZgloszeniaIT.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        [ForeignKey("Deparment")]
        public Guid IdDepartment { get; set; }
        public virtual ICollection<Report> Report { get; set; }

        public string GetUserFirstName()
        {
            return FirstName;
        }

        public string GetUserLastName()
        {
            return LastName;
        }
    }
}
