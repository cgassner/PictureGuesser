using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PictureGuessing.Models
{
    public class PictureGuessingDbContext : DbContext
    {
        public PictureGuessingDbContext(DbContextOptions<PictureGuessingDbContext> options) : base(options)
        {

        }
        public DbSet<Game> Game { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }
    }
}
