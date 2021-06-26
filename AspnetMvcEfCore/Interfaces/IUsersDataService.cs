using DomainRegistrarWebApp.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    internal interface IUsersDataService
    {
        Task<List<AppUser>> GetUsers();

        AppUser GetUser(AppUser u);

        AppUser GetUser(string username);

        Task<bool> AddUser(AppUser u);

        Task AddUsersByTransaction(IEnumerable<AppUser> users);
        //void UpdateUser(AppUser user, AppUser changedUser);
        void Task<UpdateUser>(AppUser user, AppUser changedUser);
    }
}