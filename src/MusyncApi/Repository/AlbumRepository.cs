using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using musync.api.Repository.Interfaces;
using MongoDB.Bson;
using Musync.Domain.Models;
using Musync.Domain.Services.Interfaces;

namespace musync.api.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IAlbumService _albumService;

        public AlbumRepository(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public long DeleteById(ObjectId id)
        {
            return _albumService.Delete(id);
        }

        public IQueryable<Album> GetAll()
        {
            return _albumService.GetAll();
        }

        public Album GetById(ObjectId id)
        {
            return _albumService.GetById(id);
        }

        public short Insert(Album model)
        {
            return _albumService.Insert(model);
        }

        public long SuperDelete(ObjectId id)
        {
            return _albumService.SuperDelete(id);
        }
    }
}
