using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;

namespace musync.api.Repository.Interfaces
{
    public interface IUserRepository
    {
        long DeleteById(ObjectId id);

        IQueryable<User> GetAll();

        User GetById(ObjectId id);

        short Insert(User model);

        long SuperDelete(ObjectId id);

        long ChangePassword(ObjectId id, string oldPAssword, string newPassword);
    }
}
