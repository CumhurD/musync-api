using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Musync.Domain.Models;

namespace musync.api.Repository.Interfaces
{
    public interface IAlbumRepository
    {
        long DeleteById(ObjectId id);

        IQueryable<Album> GetAll();

        Album GetById(ObjectId id);

        short Insert(Album model);

        long SuperDelete(ObjectId id);
    }
}
