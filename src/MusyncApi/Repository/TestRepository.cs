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
        ITestRepository _testRepository;

        public TestRepository(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public DeleteResult DeleteById(ObjectId id)
        {
            return _testRepository.DeleteById(id);
        }

        public List<TestModel> GetAll()
        {
            return _testRepository.GetAll().ToList();
        }

        public TestModel GetById(ObjectId id)
        {
            return _testRepository.GetById(id);
        }

        public void Insert(TestModel model)
        {
            _testRepository.Insert(model);
        }

        public UpdateResult UpdateById(ObjectId id, UpdateDefinition<TestModel> update)
        {
            var result = _testRepository.UpdateById(id, update);

            return result;
        }
    }
}
