using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;
using FluentAssertions;
using musync.api.Controllers;
using musync.api.Repository;
using musync.api.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Musync.Domain.Services;
using NUnit.Framework;

namespace musync.api.tests.UserControllerTests
{
    [TestFixture]
    public class When__Change_Password
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
        public void Should_Throw_HttpResponseException_When_OldPassword_Or_NewPassword_Is_NullOrWhiteSpace()
        {
            ObjectId id = new ObjectId();

            string oldPassword = "";

            string newPassword = "";

            var exception = Assert.Throws<HttpResponseException>(() => _userController.ChangePassword(id, oldPassword, newPassword));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_An_Exception()
        {
            ObjectId objectId = new ObjectId();

            var oldPassword = "name";

            var newPassword = "test";

            _mockedUserRepository.Setup(x => x.ChangePassword(It.IsAny<ObjectId>(), It.IsAny<String>(), It.IsAny<String>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _userController.ChangePassword(objectId, oldPassword, newPassword));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
