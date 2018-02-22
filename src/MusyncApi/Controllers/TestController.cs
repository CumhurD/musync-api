using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Musync.Domain.Service;
using MongoDB.Bson;
using Musync.Domain.Managers;
using Musync.Domain.Models;
using MongoDB.Driver;
using musync_api.Models;

namespace musync_api.Controllers
{

    [Route("api/Test")]
    public class TestController : Controller
    {
        TestService service = new Musync.Domain.Service.TestService(new MongoRepository<TestModel>());

        [HttpGet]
        public IEnumerable<Models.Test> Get()
        {
            var result = service.GetAll().Select(x => new Models.Test()
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });

            return result;
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                ObjectId objectId = new ObjectId(id);

                var result = service.GetById(objectId);
                Test test = new Test()
                {
                    Id = result.Id.ToString(),
                    Name = result.Name
                };

                return Ok(test);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public void Insert([FromBody]Test value)
        {
            if (value != null)
            {
                TestModel test = new TestModel()
                {
                    Name = value.Name
                };

                service.Insert(test);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ObjectId objectId = new ObjectId(id);
                var update = Builders<TestModel>.Update.Set(key, value);

                var result = service.Update(objectId, update);

                return Ok(result);

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ObjectId objectId = new ObjectId(id);

                var result = service.DeleteById(objectId);

                return Ok(result);
            }
            else
            {
                return BadRequest();

            }
        }
    }

}
