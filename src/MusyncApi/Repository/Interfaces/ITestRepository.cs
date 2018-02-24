using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;

namespace musync.api.Repository.Interfaces
{
    public interface ITestRepository
    {
        List<TestModel> GetAll();

        TestModel GetById(ObjectId id);

        void Insert(TestModel model);

        UpdateResult UpdateById(ObjectId id, UpdateDefinition<TestModel> update);

        DeleteResult DeleteById(ObjectId id);





    }
}
