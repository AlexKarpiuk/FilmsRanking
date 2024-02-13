using FilmsRanking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace FilmsRanking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MediaContent> MediaContent { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<WishList> WishList { get; set; }
    }
}
