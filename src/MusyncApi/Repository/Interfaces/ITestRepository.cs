using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musync.api.Repository.Interfaces
{
    public interface ITestRepository
    {
        DeleteResult DeleteById(ObjectId id);

        List<TestModel> GetAll();

        TestModel GetById(ObjectId id);

        void Insert(TestModel model);

        UpdateResult UpdateById(ObjectId id, UpdateDefinition<TestModel> update);

    }
}
