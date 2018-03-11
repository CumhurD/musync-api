using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;
using Musync.Domain.Services.Interfaces;

namespace musync.api.Controllers
{
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userRepository;

        public UserController(IUserService userRepository)
        {
            _userRepository = userRepository;

        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<UserVM> Get()
        {
            return _userRepository.GetAll().Select(x => new UserVM
            {
                id = x.Id,
                DisplayName = x.DisplayName,
                Birthdate = x.Birthdate,
                Password = x.Password,
                Country = x.Country,
                EmailAdress = x.EmailAdress
            }).ToList();

        }



        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public UserVM Get(ObjectId id)
        {
            try
            {
                var result = _userRepository.GetById(id);

                UserVM user = new UserVM()
                {
                    id = result.Id,
                    Birthdate = result.Birthdate,
                    Password = result.Password,
                    EmailAdress = result.EmailAdress,
                    Country = result.Country,
                    DisplayName = result.DisplayName
                };

                return user;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost()]
        public void Insert([Microsoft.AspNetCore.Mvc.FromBody]UserVM value)
        {
            if (value != null)
            {
                try
                {
                    User user = new User()
                    {
                        DisplayName = value.DisplayName,
                        Password = value.Password,
                        EmailAdress = value.EmailAdress,
                        Country = value.Country,
                        Birthdate = value.Birthdate
                    };

                    _userRepository.Insert(user);
                }
                catch
                {

                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPut()]
        public long Delete(ObjectId id)
        {
            try
            {
                return _userRepository.Delete(id);
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
                return _userRepository.SuperDelete(id);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPut()]
        public long Update(ObjectId id, string oldPassword, string newPassword)
        {
            if (String.IsNullOrWhiteSpace(oldPassword) || String.IsNullOrWhiteSpace(newPassword))
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            try
            {
                return _userRepository.ChangePassword(id, oldPassword, newPassword);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
