using FilmsRanking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace FilmsRanking.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<MediaContent> MediaContent { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<WishList> WishList { get; set; }
    }
}
