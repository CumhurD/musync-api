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

        //[Microsoft.AspNetCore.Mvc.HttpGet]
        //public List<UserVM> Get()
        //{
        //    var result = _userRepository.GetAll();



        //    result.GetEnumerator();
        //    return result;
        //}

        //[Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        //public Test Get(ObjectId id)
        //{
        //    try
        //    {
        //        var result = _userRepository.GetById(id);

        //        Test test = new Test()
        //        {
        //            Id = result.Id,
        //            Name = result.Name
        //        };

        //        return test;
        //    }
        //    catch
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //}

        //[Microsoft.AspNetCore.Mvc.HttpPost()]
        //public void Insert([Microsoft.AspNetCore.Mvc.FromBody]Test value)
        //{
        //    if (value != null)
        //    {
        //        try
        //        {
        //            TestModel testModel = new TestModel()
        //            {
        //                Name = value.Name
        //            };

        //            _userRepository.Insert(testModel);
        //        }
        //        catch
        //        {

        //            throw new HttpResponseException(HttpStatusCode.BadRequest);
        //        }

        //    }
        //}

        //[Microsoft.AspNetCore.Mvc.HttpPut()]
        //public UpdateResult Update(ObjectId id, string key, string value)
        //{
        //    try
        //    {

        //        var update = Builders<TestModel>.Update.Set(key, value);

        //        var result = _userRepository.UpdateById(id, update);

        //        return result;

        //    }
        //    catch
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }

        //}

        //[Microsoft.AspNetCore.Mvc.HttpDelete()]
        //public DeleteResult Delete(ObjectId id)
        //{
        //    try
        //    {
        //        var result = _userRepository.DeleteById(id);

        //        return result;
        //    }
        //    catch
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //}
    }
}
