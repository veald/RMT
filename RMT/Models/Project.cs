using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RMT.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Display(Name = "Projet")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<Picture> Pictures { get; set; }
        
    }
}