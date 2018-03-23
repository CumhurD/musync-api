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
    public class SongRepository : ISongRepository
    {
        private readonly ISongService _songService;

        public SongRepository(ISongService songService)
        {
            _songService = songService;
        }

        public long DeleteById(ObjectId id)
        {
            return _songService.Delete(id);
        }

        public IQueryable<Song> GetAll()
        {
            return _songService.GetAll();
        }

        public Song GetById(ObjectId id)
        {
            return _songService.GetById(id);
        }

        public short Insert(Song model)
        {
            return _songService.Insert(model);
        }

        public long SuperDelete(ObjectId id)
        {
            return _songService.SuperDelete(id);
        }
    }
}
