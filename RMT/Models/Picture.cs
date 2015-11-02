using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Models
{
    public class Picture
    {
        public int PictureId { get; set; }

        [Display(Name="Pseudo")]
        public string PictureName { get; set; }

        public string Description{ get; set; }
        
        public string Path { get; set; }

        //[ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        
        public Project Project { get; set; }

        //public ICollection<Comment> Comments { get; set; }

    }
}