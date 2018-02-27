using System;
using System.Net;
using System.Web.Http;
using FluentAssertions;
using musync.api.Controllers;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using MongoDB.Bson;
using Moq;
using Musync.Domain.Models;
using NUnit.Framework;

namespace musync.api.tests.TestControllerTests
{
    [TestFixture]
    public class When_Insert
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
        public void Should_Not_Call_Method_If_Value_Is_Null()
        {
            Test test = null;

            TestModel testModel = null;

            _testController.Insert(test);

            _mockedTestRepository.Verify(x => x.Insert(testModel), Times.Never);
        }


        [Test]
        public void Should_Throw_BadRequest_If_There_Is_A_Exception()
        {
            Test test = new Test()
            {
                Name = "test"
            };

            _mockedTestRepository.Setup(x => x.Insert(It.IsAny<TestModel>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _testController.Insert(test));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
