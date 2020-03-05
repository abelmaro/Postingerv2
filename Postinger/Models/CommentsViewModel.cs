using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Postinger.Models
{
    public class CommentsViewModel
    { 
        public int Id { get; set; }
        public string Comentario { get; set; }

        [ForeignKey("PostID")]
        public virtual PostViewModel Post { get; set; }

        public int PostID { get; set; }

        public string Usuario { get; set; }

        [ForeignKey("Usuario")]
        public UserViewModel User { get; set; }

        public DateTime FechaComentario { get; set; }
    }
}
