using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Musync.Domain.Service;
using MongoDB.Bson;
using Musync.Domain.Managers;
using Musync.Domain.Models;
using MongoDB.Driver;
using musync_api.Models;
using Musync_api.Repository.Interfaces;

namespace musync_api.Controllers
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
        public ActionResult Get(string id)
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

                return Ok(test);
            }
            catch
            {
                return BadRequest();
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
