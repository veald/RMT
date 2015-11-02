using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RMT.Models
{
    public class Comment
    {   
        public int CommentId { get; set; }
        
        public int PictureId { get; set; }

        [Display(Name = "Pseudo")]
        public string UserName { get; set; }

        [Display(Name = "Sujet")]
        public string Subject { get; set; }

        [Display(Name = "Commentaire")]
        public string Body { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public bool Hidden { get; set; }

        public Picture Picture { get; set; }
        
    }
}