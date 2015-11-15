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
        [Display(Name = "Statut")]
        public string Status { get; set; }

        [Display(Name = "Date de début")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "Date de fin")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        public ICollection<Picture> Pictures { get; set; }
        
    }
}