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
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class PostController : ControllerBase
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

        /// <summary>
        /// Añade un nuevo Post.
        /// </summary>
        /// <param name = "postData" ></ param >
        [HttpPost]
        public void AddNewPost([FromBody] PostViewModel postData)
        {
            if (postData != null)
            {
                _context.Add(postData);
                _context.SaveChanges();
            }
        }

        ///// <summary>
        ///// Obtiene todos los posts por usuario
        ///// </summary>
        //[HttpGet]
        //public List<PostViewModel> GetAll() //TODO: Agregar AutoMapper
        //{
        //    //var userID = GetCurrentUserId();
        //    var userID = "4ed9f1f1-96c8-498e-b3dd-c8d57b4115c1";

        //    var posts = _context.Post
        //        //.Include(x => x.Comentarios)
        //        //    .ThenInclude(x => x.User)
        //        //.Include(x => x.Likes)
        //        .Where(x => x.UserId == userID);

        //    var test = posts.ToList();

        //    foreach (var item in test)
        //    {
        //        item.cantidadLike = _context.Like.Where(x => x.PostId == item.Id && x.Tipo == "like").Count();
        //        item.cantidadDislike = _context.Like.Where(x => x.PostId == item.Id && x.Tipo == "dislike").Count();
        //    }

        //    return test;
        //}

        /// <summary>
        /// Obtiene todos los posts
        /// </summary>
        [HttpGet]
        public List<PostViewModel> AllPosts()
        {
            //return _context.Post.GroupBy(x => x.UserId).ToList();
            var posts = _context.Post
                .Include(x => x.User)
                .ToList();

            foreach (var item in posts)
            {
                item.cantidadLike = _context.Like.Where(x => x.PostId == item.Id && x.Tipo == "like").Count();
                item.cantidadDislike = _context.Like.Where(x => x.PostId == item.Id && x.Tipo == "dislike").Count();
            }
            return posts;
        }

        /// <summary>
        /// Obtiene el id del usuario logueado
        /// </summary>
        [HttpGet]
        public string GetCurrentUserId()
        {
            var identity = (ClaimsIdentity)User.Identity;
            //var user = _UserManager.GetUserId();
            var user = _UserManager.GetUserAsync(HttpContext.User);



            IEnumerable<Claim> claims = identity.Claims;

            //var userLogged = _context.Users.Where(x => x.UserName == user.Result.UserName).FirstOrDefault();

            //var id = userLogged.Id;

            return "4ed9f1f1-96c8-498e-b3dd-c8d57b4115c1";
        }

        /// <summary>
        /// Añade un like a un post
        /// </summary>
        [HttpPost]
        public void AddLike([FromBody] LikesViewModel vm)
        {
            var likes = _context.Like;
            var validar = likes.Where(x => x.PostId == vm.PostId && x.UserId == vm.UserId).Count() > 0;
            if (validar == false)
            {
                var like = likes.Add(vm);
                _context.SaveChanges();
            }
        }
    }
}
