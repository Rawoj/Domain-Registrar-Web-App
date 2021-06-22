using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DomainRegistrarWebApp.Database
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options)
        {
        }

        protected UsersContext()
        {
        }

        public DbSet<User> Users { get; set; }
    }
}