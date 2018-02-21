using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusyncApi.Models;

namespace musync_api.Controllers
{
    [Route("api/Test")]
    public class TestController : Controller
    {
        [HttpGet]
        public IEnumerable<TestModel> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public TestModel Get(int id)
        {
            return null;
        }

        [HttpPost]
        public void Post([FromBody]TestModel value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TestModel value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}