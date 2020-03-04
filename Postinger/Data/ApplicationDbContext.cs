using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Postinger.Models;

namespace Postinger.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserViewModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PostViewModel> Post { get; set; }
        public DbSet<CommentsViewModel> Comment { get; set; }
    }
}
