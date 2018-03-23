using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Musync.Domain.Models;

namespace musync.api.Controllers
{
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("api/Album")]
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        public AlbumController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<AlbumVM> Get()
        {
            return _albumRepository.GetAll().Select(x => new AlbumVM
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                ArtistsIdentities = x.ArtistsIdentities,
                SongIdentities = x.SongIdentities

            }).ToList();
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public AlbumVM Get(ObjectId id)
        {
            try
            {
                var result = _albumRepository.GetById(id);

                AlbumVM album = new AlbumVM()
                {
                    Id = result.Id,
                    DisplayName = result.DisplayName,
                    ArtistsIdentities = result.ArtistsIdentities,
                    SongIdentities = result.SongIdentities
                };

                return album;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost()]
        public short Insert([Microsoft.AspNetCore.Mvc.FromBody]AlbumVM value)
        {
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                Album album = new Album()
                {
                    DisplayName = value.DisplayName,
                    ArtistsIdentities = value.ArtistsIdentities,
                    SongIdentities = value.SongIdentities
                };

                return _albumRepository.Insert(album);
            }
            catch
            {

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

        [Microsoft.AspNetCore.Mvc.HttpPut()]
        public long Delete(ObjectId id)
        {
            try
            {
                return _albumRepository.DeleteById(id);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

        [Microsoft.AspNetCore.Mvc.HttpDelete()]
        public long SuperDelete(ObjectId id)
        {
            try
            {
                return _albumRepository.SuperDelete(id);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}