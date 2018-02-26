using FluentAssertions;
using MongoDB.Bson;
using Moq;
using musync.api.Controllers;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using Musync.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace musync.api.tests.TestControllerTests
{
    [TestFixture]
    public class When_Get_By_Id
    {
        private Mock<ITestRepository> _mockedTestController;

        private TestController _testController;

        [SetUp]
        public void Setup()
        {
            _mockedTestController = new Mock<ITestRepository>();

            _testController = new TestController(_mockedTestController.Object);
        }

        [Test]
        public void Should_Return_IEnumerable_Test_Properly()
        {
            ObjectId objectId = new ObjectId();

            TestModel testModel = new TestModel()
            {
                Id = objectId,
                Name = "test"
            };

            Test test = new Test()
            {
                Id = testModel.Id,
                Name = testModel.Name
            };

            _mockedTestController.Setup(x => x.GetById(objectId)).Returns(testModel);

            _testController.Get(objectId).Should().Be(test);
        }
    }
}
