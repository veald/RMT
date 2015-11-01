using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RMT.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
            : base("DefaultConnection") 
        {
                   
        }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}