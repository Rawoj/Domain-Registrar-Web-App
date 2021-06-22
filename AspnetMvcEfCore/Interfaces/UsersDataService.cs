﻿using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    public class UsersDataService : IUsersDataService
    {
        private readonly DatabaseContext _db;

        public UsersDataService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> AddUser(User u)
        {
            try
            {
                _db.Users.Add(u);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task AddUsersByTransaction(IEnumerable<User> users)
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

        public async Task<List<User>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }
    }
}