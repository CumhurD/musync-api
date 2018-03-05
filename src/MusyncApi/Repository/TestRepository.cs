using System.Collections.Generic;
using System.Linq;
using musync.api.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;
using Musync.Domain.Services;

namespace musync.api.Repository
{
    public class TestRepository : ITestRepository
    {
        ITestService _testService;

        public TestRepository(ITestService testService)
        {
            _testService = testService;
        }

        public DeleteResult DeleteById(ObjectId id)
        {
            return _testService.DeleteById(id);
        }

        public List<TestModel> GetAll()
        {
            return _testService.GetAll().ToList();
        }

        public TestModel GetById(ObjectId id)
        {
            return _testService.GetById(id);
        }

        public void Insert(TestModel model)
        {
            _testService.Insert(model);
        }

        public UpdateResult UpdateById(ObjectId id, UpdateDefinition<TestModel> update)
        {
            var result = _testService.UpdateById(id, update);

            return result;
        }
    }
}
