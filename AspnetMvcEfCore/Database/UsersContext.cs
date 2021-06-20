using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Database
{
    public class UsersContext: DbContext
    {
        public UsersContext(DbContextOptions options) : base(options) { }
        protected UsersContext() { }
        public DbSet<User> Users { get; set; }
    }
}
