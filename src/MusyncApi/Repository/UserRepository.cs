using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;
using Musync.Domain.Services.Interfaces;

namespace musync.api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserService _userService;

        public UserRepository(IUserService userService)
        {
            _userService = userService;
        }

        public long DeleteById(ObjectId id)
        {
            return _userService.Delete(id);
        }

        public IQueryable<User> GetAll()
        {
            return _userService.GetAll();
        }

        public User GetById(ObjectId id)
        {
            return _userService.GetById(id);
        }

        public short Insert(User model)
        {
            return _userService.Insert(model);
        }

        public long SuperDelete(ObjectId id)
        {
            return _userService.SuperDelete(id);
        }

        public long ChangePassword(ObjectId id, string oldPAssword, string newPassword)
        {
            return _userService.ChangePassword(id, oldPAssword, newPassword);
        }
    }
}
