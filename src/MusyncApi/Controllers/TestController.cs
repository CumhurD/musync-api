using System.Collections.Generic;
using System.Linq;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;

namespace musync.api.Controllers
{

    [Route("api/Test")]
    public class TestController : Controller
    {
       private ITestRepository _testRepository;

        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        [HttpGet]
        public IEnumerable<Test> Get()
        {
            var result = _testRepository.GetAll().Select(x => new Test()
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });

            return result;
        }

        [HttpGet("{id}")]
        public Test Get(string id)
        {

            try
            {
                ObjectId objectId = new ObjectId(id);

                var result = _testRepository.GetById(objectId);

                Test test = new Test()
                {
                    Id = result.Id.ToString(),
                    Name = result.Name
                };

                return test;
            }
            catch
            {
                throw new HttpResponseException();
            }
        }

        [HttpPost("Insert")]
        public void Insert([FromBody]Test value)
        {
            if (value != null)
            {
                TestModel test = new TestModel()
                {
                    Name = value.Name
                };

                _testRepository.Insert(test);
            }
        }

        [HttpPut("Update")]
        public ActionResult Update(string id, string key, string value)
        {
            try
            {

                ObjectId objectId = new ObjectId(id);

                var test = _testRepository.GetById(objectId);

                var update = Builders<TestModel>.Update.Set(key, value);

                var result = _testRepository.UpdateById(objectId, update);

                return Ok(result);

            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string id)
        {
            try
            {
                ObjectId objectId = new ObjectId(id);

                var result = _testRepository.DeleteById(objectId);

                return Ok(result);
            }
            catch
            {
                return BadRequest();

            }
        }
    }

}
