using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Musync.Domain.Models;

namespace musync.api.Repository.Interfaces
{
    public interface ISongRepository
    {
        long DeleteById(ObjectId id);

        IQueryable<Song> GetAll();

        Song GetById(ObjectId id);

        short Insert(Song model);

        long SuperDelete(ObjectId id);
    }
}
