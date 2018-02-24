using System.Collections.Generic;
using System.Linq;
using musync.api.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Managers;
using Musync.Domain.Models;
using Musync.Domain.Service;

namespace musync.api.Repository
{
    public class TestRepository : BaseMusicRepository, ITestRepository
    {
        TestService service = new TestService(new MongoRepository<TestModel>());

        public DeleteResult DeleteById(ObjectId id)
        {
            return service.DeleteById(id);
        }

        public List<TestModel> GetAll()
        {
            return service.GetAll().ToList();
        }

        public TestModel GetById(ObjectId id)
        {
            return service.GetById(id);
        }

        public void Insert(TestModel model)
        {
            service.Insert(model);
        }

        public UpdateResult UpdateById(ObjectId id, UpdateDefinition<TestModel> update)
        {
            var result = service.UpdateById(id, update);

            return result;
        }
    }
}
