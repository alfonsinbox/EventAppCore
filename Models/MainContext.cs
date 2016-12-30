using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EventAppCore.Models
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Using Environment variable {0}", Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb"));
            //optionsBuilder.UseMySql(Environment.GetEnvironmenVariable("MYSQLCONNSTR_localdb"));
            //optionsBuilder.UseMySql("Server=localhost;Database=EventApp;Uid=root");
            optionsBuilder.UseMySql("Server=127.0.0.1:55954;Database=localdb;Uid=azure;Password=6#vWHD_$");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Location>()
                .HasIndex(l => l.Name)
                .IsUnique();

            /*modelBuilder.Entity<Category>()
                .HasMany(c => c.UsersWithInterest);*/
            /*modelBuilder.Entity<UserEvent>()
                .HasKey(ue => new {ue.User.Id, ue.Event.Id});*/
        }
    }
}
