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
    [Microsoft.AspNetCore.Mvc.Route("api/Song")]
    public class SongController : Controller
    {
        private readonly ISongRepository _songRepository;

        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<SongVM> Get()
        {
            return _songRepository.GetAll().Select(x => new SongVM
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                AlbumId = x.AlbumId,
                ArtistId = x.ArtistId,
            }).ToList();
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public SongVM Get(ObjectId id)
        {
            try
            {
                var result = _songRepository.GetById(id);

                SongVM user = new SongVM()
                {
                    Id = result.Id,
                    DisplayName = result.DisplayName,
                    ArtistId = result.ArtistId,
                    AlbumId = result.AlbumId
                };

                return user;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost()]
        public short Insert([Microsoft.AspNetCore.Mvc.FromBody]SongVM value)
        {
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                Song user = new Song()
                {
                    DisplayName = value.DisplayName,
                    ArtistId = value.ArtistId,
                    AlbumId = value.AlbumId,
                };

                return _songRepository.Insert(user);
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
                return _songRepository.DeleteById(id);
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
                return _songRepository.SuperDelete(id);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}