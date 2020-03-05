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
    //[Authorize]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<UserViewModel> _UserManager;
        //private readonly PostController _postController;

        public UserController(ApplicationDbContext context, ILogger<UserController> logger, UserManager<UserViewModel> userManager/*, PostController postController*/)
        {
            _context = context;
            _logger = logger;
            _UserManager = userManager;
            //_postController = postController;
        }

        public IActionResult User()
        {
            return View();
        }

        public string GetCurrentUserId() //TODO: Reemplazar por metodo de PostController por medio de Inyección
        {
            var user = _UserManager.GetUserAsync(HttpContext.User);

            var userLogged = _context.Users.Where(x => x.UserName == user.Result.UserName).FirstOrDefault();

            var id = userLogged.Id;

            return id;
        }

        public List<UserViewModel> GetAllUsers()
        {
            var userExclude = GetCurrentUserId();
            var users = _context.Users.Where(x => x.Id != userExclude).ToList();

            return users;
        }
    }
}
