using Postinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postinger
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string PostName { get; set; }

        public string Autor { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string UserId { get; set; }

        public virtual UserViewModel User { get; set; }

        public IList<CommentsViewModel> Comentarios { get; set; }

        public int cantidadLike { get; set; }

        public int cantidadDislike { get; set; }
    }
}
