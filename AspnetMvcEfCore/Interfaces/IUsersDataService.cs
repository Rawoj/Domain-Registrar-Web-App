using DomainRegistrarWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    interface IUsersDataService
    {
        Task<List<User>> GetUsers();
        Task<bool> AddUser(User u);
        Task AddUsersByTransaction(IEnumerable<User> users);
    }
}
