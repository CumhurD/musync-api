using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using musync.api.Controllers;
using musync.api.Repository.Interfaces;
using Musync.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;

namespace musync.api.tests.TestControllerTests
{
    [TestFixture]
    public class When_Update
    {
        private Mock<ITestRepository> _mockedTestRepository;

        private TestController _testController;

        [SetUp]
        public void Setup()
        {
            _mockedTestRepository = new Mock<ITestRepository>();

            _testController = new TestController(_mockedTestRepository.Object);
        }

        [Test]
        public void Should_Update_Item()
        {
            ObjectId objectId = new ObjectId();

            var key = "name";

            var value = "test";

            _mockedTestRepository.Setup(x => x.UpdateById(It.IsAny<ObjectId>(), It.IsAny<UpdateDefinition<TestModel>>())).Returns(new UpdateResult.Acknowledged(1, 1, objectId));

            _testController.Update(objectId, key, value).ModifiedCount.Should().Be(1);
        }


        [Test]
        public void Should_Throw_BadRequest_If_There_Is_A_Exception()
        {
            ObjectId objectId = new ObjectId();

            var key = "name";

            var value = "test";

            _mockedTestRepository.Setup(x => x.UpdateById(It.IsAny<ObjectId>(), It.IsAny<UpdateDefinition<TestModel>>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _testController.Update(objectId,key,value));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
