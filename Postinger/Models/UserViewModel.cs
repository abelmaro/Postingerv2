using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postinger.Models
{
    public class UserViewModel : IdentityUser
    { 
        public virtual IEnumerable<PostViewModel> Posts { get; set; }
        public virtual IEnumerable<CommentsViewModel> Comentarios { get; set; }
    }
}
