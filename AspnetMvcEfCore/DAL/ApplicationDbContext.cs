using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;


namespace DomainRegistrarWebApp.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
            .HasIndex(p => new { p.Username, p.Email, p.Id })
            .IsUnique(true);

            modelBuilder.Entity<BoughtDomain>()
            .HasIndex(p => new { p.Id })
            .IsUnique(true);
        }


        public DbSet<AppUser> Users { get; set; }
        public DbSet<BoughtDomain> BoughtDomains { get; set; }


    }
}