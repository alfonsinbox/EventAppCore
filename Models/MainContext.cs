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

        private readonly IHostingEnvironment _environment;
        public MainContext(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Console.WriteLine("Using Environment variable {0}",
              //  Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb"));
            //optionsBuilder.UseMySql(Environment.GetEnvironmenVariable("MYSQLCONNSTR_localdb"));
            //optionsBuilder.UseMySql("Server=localhost;Database=EventApp;Uid=root");
            //optionsBuilder.UseMySql("Database=localdb;Data Source=127.0.0.1:55954;User Id=azure;Password=6#vWHD_$");
            //optionsBuilder.UseMySql("Database=eventappdb;Data Source=eu-cdbr-azure-west-a.cloudapp.net;User Id=b8eec197bc2043;Password=a9669c43");
            if (_environment.IsEnvironment("LocalMac"))
            {
                //optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb"));
                optionsBuilder.UseSqlServer(
                    "Server=tcp:eventdbserver.database.windows.net,1433;Initial Catalog=eventappcoredb;Persist Security Info=False;User ID=eventadmin;Password=eVent123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
            else if (_environment.IsDevelopment())
            {
                optionsBuilder.UseSqlServer(
                    "Server=tcp:eventdbserver.database.windows.net,1433;Initial Catalog=eventappcoredb;Persist Security Info=False;User ID=eventadmin;Password=eVent123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
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