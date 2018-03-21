using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;
using FluentAssertions;
using musync.api.Controllers;
using musync.api.Repository.Interfaces;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;

namespace musync.api.tests.UserControllerTests
{
    [TestFixture]
    public class When_Delete
    {
        private Mock<IUserRepository> _mockedUserRepository;

        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _mockedUserRepository = new Mock<IUserRepository>();

            _userController = new UserController(_mockedUserRepository.Object);
        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_An_Exception()
        {
            ObjectId objectId = new ObjectId();

            _mockedUserRepository.Setup(x => x.DeleteById(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _userController.Delete(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
