using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Postinger.Data;
using Postinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Identity;


namespace Postinger.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<UserViewModel> _UserManager;

        public PostController(ApplicationDbContext context, ILogger<PostController> logger, UserManager<UserViewModel> userManager)
        {
            _context = context;
            _logger = logger;
            _UserManager = userManager;
        }

        public IActionResult Post()
        {
            var postDb = _context.Post.ToList();
            return View(postDb);
        }

        //[HttpPost]
        public void AddNewPost([FromBody] PostViewModel postData)
        {
            if (postData != null)
            {
                _context.Add(postData);
                _context.SaveChanges();
            }
        }

        //[HttpGet]
        //[AllowAnonymous]
        public List<PostViewModel> GetAll()
        {
            var userID = GetCurrentUserId();

            var post = _context.Post
                .Include(x => x.Comentarios)
                    .ThenInclude(x => x.User)
                .Where(x => x.UserId == userID)
                .ToList();

            return post;
        }

        public List<PostViewModel> AllPosts()
        {
            //return _context.Post.GroupBy(x => x.UserId).ToList();
            return _context.Post
                .Include(x => x.Comentarios)
                .ThenInclude(x => x.User)
                .ToList();
        }


        //public IdentityUser TheUser(string email)
        //{
        //    var user = _context.Users.Where(x => x.UserName == email).FirstOrDefault();

        //    return user;
        //}

        //public list<commentsviewmodel> getcomentariosbypostid(int id)
        //{
        //    var comentarios = _context.comment.where(x => x.postid == id).tolist();
        //    return comentarios;
        //}

        public string GetCurrentUserId()
        {
            var identity = (ClaimsIdentity)User.Identity;
            //var user = _UserManager.GetUserId();
            var user = _UserManager.GetUserAsync(HttpContext.User);



            IEnumerable<Claim> claims = identity.Claims;

            var userLogged = _context.Users.Where(x => x.UserName == user.Result.UserName).FirstOrDefault();

            var id = userLogged.Id;

            return id;
        }
    }
}
