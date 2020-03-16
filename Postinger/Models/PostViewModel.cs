using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Postinger.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string PostName { get; set; }

        public string Autor { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserViewModel User { get; set; }

        public IList<CommentsViewModel> Comentarios { get; set; }

        public IList<LikesViewModel> Likes { get; set; }

        [NotMapped]
        public int cantidadLike { get; set; }

        [NotMapped]
        public int cantidadDislike { get; set; }


    }
}
