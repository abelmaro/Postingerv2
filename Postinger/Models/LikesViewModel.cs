using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Postinger.Models
{
    public class LikesViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserViewModel User { get; set; }

        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public PostViewModel Post { get; set; }

        public string Tipo { get; set; }

    }
}
