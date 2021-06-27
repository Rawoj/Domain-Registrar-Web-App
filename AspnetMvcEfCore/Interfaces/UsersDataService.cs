using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    public class UsersDataService : IUsersDataService
    {
        private readonly ApplicationDbContext _db;

        public UsersDataService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddUser(AppUser u)
        {
            try
            {
                await _db.Users.AddAsync(u);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task AddUsersByTransaction(IEnumerable<AppUser> users)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                foreach (var u in users)
                {
                    _db.Users.Add(u);
                    await _db.SaveChangesAsync();
                }

                await _db.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _db.Database.RollbackTransactionAsync();
            }
        }

        public async Task<List<AppUser>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public AppUser GetUser(string username)
        {
            var q =_db.Users.FirstOrDefaultAsync(s => s.Username == username);

            return q.Result;
        }

        public AppUser GetUser(AppUser u)
        {
            return GetUser(u.Username);
        }

        public async void Task<UpdateUser>(AppUser u, AppUser changedUser)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = GetUser(u);

                // User not found
                if (user == null)
                {
                    await _db.Database.RollbackTransactionAsync();
                    return;
                }

                _db.Update(user);
                user = changedUser;
                await _db.SaveChangesAsync();
                await _db.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _db.Database.RollbackTransactionAsync();
            }
        }
            

        public async void Task<AddBalance>(AppUser u, decimal amount)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = GetUser(u);

                // User not found
                if (user == null)
                {
                    await _db.Database.RollbackTransactionAsync();
                    return;
                }

                _db.Update(user);
                user.Balance += amount;


                await _db.SaveChangesAsync();
                await _db.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _db.Database.RollbackTransactionAsync();
            }

        }


        public async void Task<RemoveUser>(AppUser u)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = GetUser(u);

                // User not found
                if (user == null)
                {
                    await _db.Database.RollbackTransactionAsync();
                    return;
                }

                _db.Remove(user);

                await _db.SaveChangesAsync();
                await _db.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _db.Database.RollbackTransactionAsync();
            }

        }


    }
}