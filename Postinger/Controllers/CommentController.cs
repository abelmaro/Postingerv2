using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Postinger.Data;
using Postinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Postinger.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserViewModel> userManager;

        public CommentController(ApplicationDbContext context, UserManager<UserViewModel> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public List<CommentsViewModel> GetComentariosByPostId(int id)
        {
            var comentarios = _context.Comment.Where(x => x.PostID == id).ToList(); 
            return comentarios;
        }

        public void AddComment([FromBody] CommentsViewModel vm)
        {
            var comment = new CommentsViewModel();
            comment.Comentario = vm.Comentario;
            comment.PostID = vm.PostID;
            comment.FechaComentario = DateTime.Now;
            comment.Usuario = GetCurrentUserId();

            _context.Comment.Add(comment);
            _context.SaveChanges();
        }

        public string GetCurrentUserId()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var user = userManager.GetUserAsync(HttpContext.User);

            IEnumerable<Claim> claims = identity.Claims;

            var userLogged = _context.Users.Where(x => x.UserName == user.Result.UserName).FirstOrDefault();

            var id = userLogged.Id;

            return id;
        }

    }
}
