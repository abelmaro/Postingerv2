﻿using Microsoft.AspNetCore.Cors;
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
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserViewModel> userManager;

        public CommentController(ApplicationDbContext context, UserManager<UserViewModel> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public List<CommentsViewModel> GetComentariosByPostId(int id)
        {
            var comentarios = _context.Comment.Where(x => x.PostID == id).ToList(); 
            return comentarios;
        }
        [HttpPost]
        public CommentsViewModel AddComment([FromBody] CommentsViewModel vm)
        {
            var comment = new CommentsViewModel();
            comment.Comentario = vm.Comentario;
            comment.PostID = vm.PostID;
            comment.FechaComentario = DateTime.Now;
            //comment.Usuario = GetCurrentUserId();
            comment.Usuario = "4ed9f1f1-96c8-498e-b3dd-c8d57b4115c1";

            _context.Comment.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        [HttpGet]
        public string GetCurrentUserId()
        {
            //var identity = (ClaimsIdentity)User.Identity;
            var user = userManager.GetUserAsync(HttpContext.User);

            //IEnumerable<Claim> claims = identity.Claims;

            var userLogged = _context.Users.Where(x => x.UserName == user.Result.UserName).FirstOrDefault();

            var id = userLogged.Id;

            return id;
        }
        [HttpGet]
        public List<CommentsViewModel> GetComments()
        {
            var comentarios = _context.Comment.ToList();
            return comentarios;
        }

        [HttpDelete]
        public void DeleteComment(int id)
        {
            var comment = _context.Comment.Where(x => x.Id == id).FirstOrDefault();
            _context.Remove(comment);
            _context.SaveChanges();
        }
        
        [HttpPost]
        public void DeleteComments(int[] ids)
        {
            foreach (var id in ids)
            {
                var comment = _context.Comment.Where(x => x.Id == id).FirstOrDefault();
                _context.Remove(comment);

            }
            _context.SaveChanges();
        }

    }
}
