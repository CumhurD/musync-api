using MongoDB.Bson;
using MongoDB.Driver;
using Musync.Domain.Models;
using musync_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musync_api.Repository.Interfaces
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
