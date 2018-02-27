using System;
using System.Net;
using System.Web.Http;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using musync.api.Controllers;
using musync.api.Repository.Interfaces;
using Musync.Domain.Models;
using NUnit.Framework;


namespace musync.api.tests.TestControllerTests
{
    [TestFixture]
    public class When_Delete
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
        public void Should_Delete_Item()
        {
            ObjectId objectId = new ObjectId();

            _mockedTestRepository.Setup(x => x.DeleteById(objectId)).Returns(new DeleteResult.Acknowledged(1));

            _testController.Delete(objectId).DeletedCount.Should().Be(1);
        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_A_Exception()
        {
            ObjectId objectId = new ObjectId();

            _mockedTestRepository.Setup(x => x.DeleteById(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _testController.Delete(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

