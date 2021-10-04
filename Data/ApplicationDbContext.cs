using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ZgloszeniaIT.Models;

namespace ZgloszeniaIT.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<KnowledgeBase> KnowledgeBase  { get; set; }
        public DbSet<Report> Reports  { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
