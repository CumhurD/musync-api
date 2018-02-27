﻿using FluentAssertions;
using MongoDB.Bson;
using Moq;
using musync.api.Controllers;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using Musync.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace musync.api.tests.TestControllerTests
{
    [TestFixture]
    public class When_Get_By_Id
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
        public void Should_Return_Test_Properly()
        {
            ObjectId objectId = new ObjectId();

            string name = "test";

            TestModel testModel = new TestModel()
            {
                Id = objectId,
                Name = name
            };


            _mockedTestRepository.Setup(x => x.GetById(objectId)).Returns(testModel);

           var result=_testController.Get(objectId);

            result.Id.Should().Be(objectId);

            result.Name.Should().Be(name);

        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_A_Exception()
        {
            ObjectId objectId = new ObjectId();

            _mockedTestRepository.Setup(x => x.GetById(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _testController.Get(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
