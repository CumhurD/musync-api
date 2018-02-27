using System.Collections.Generic;
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
    public class When_Get
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
        public void Should_Return_IEnumerable_Test_Properly()
        {
            ObjectId objectId = new ObjectId();

            string name = "test";

            List<TestModel> testModelList = new List<TestModel>()
            {
                new TestModel()
                {
                    Id=objectId,
                    Name= "test"
                }
            };

            _mockedTestRepository.Setup(x => x.GetAll()).Returns(testModelList);

            var result=_testController.Get();

            result[0].Id.Should().Be(objectId);

            result[0].Name.Should().Be(name);
        }

    }
}
