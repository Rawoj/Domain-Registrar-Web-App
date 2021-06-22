using DomainRegistrarWebApp.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    internal interface IUsersDataService
    {
        Task<List<User>> GetUsers();

        Task<bool> AddUser(User u);

        Task AddUsersByTransaction(IEnumerable<User> users);
    }
}