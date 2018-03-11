using System.Collections.Generic;
using System.Linq;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;
using System.Net;
using System.Web.Http;

namespace musync.api.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/Test")]
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;

        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;

        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<Test> Get()
        {
            var result = _testRepository.GetAll().Select(x => new Test()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return result;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public Test Get(ObjectId id)
        {
            try
            {
                var result = _testRepository.GetById(id);

                Test test = new Test()
                {
                    Id = result.Id,
                    Name = result.Name
                };

                return test;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost()]
        public void Insert([Microsoft.AspNetCore.Mvc.FromBody]Test value)
        {
            if (value != null)
            {
                try
                {
                    TestModel testModel = new TestModel()
                    {
                        Name = value.Name
                    };

                    _testRepository.Insert(testModel);
                }
                catch
                {

                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPut()]
        public UpdateResult Update(ObjectId id, string key, string value)
        {
            try
            {

                var update = Builders<TestModel>.Update.Set(key, value);

                var result = _testRepository.UpdateById(id, update);

                return result;

            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete()]
        public DeleteResult Delete(ObjectId id)
        {
            try
            {
                var result = _testRepository.DeleteById(id);

                return result;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}

