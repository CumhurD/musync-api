using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using FluentAssertions;
using musync.api.Controllers;
using musync.api.Models;
using musync.api.Repository.Interfaces;
using MongoDB.Bson;
using Moq;
using Musync.Domain.Models;
using NUnit.Framework;

namespace musync.api.tests.UserControllerTests
{
    [TestFixture]
    public class When_Get_By_Id
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

            _mockedUserRepository.Setup(x => x.GetById(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _userController.Get(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Return_UserVM_Properly()
        {
            ObjectId id = new ObjectId();

            UserVM userVM = new UserVM()
            {
                Id = new ObjectId(),
                DisplayName = "test1",
                Country = "testCountry",
                Birthdate = DateTime.Today,
                Password = "123",
                EmailAdress = "testEmail1"
            };

            User user = new User()
            {
                Id = new ObjectId(),
                DisplayName = "test1",
                Country = "testCountry",
                Birthdate = DateTime.Today,
                Password = "123",
                EmailAdress = "testEmail1"
            };

            _mockedUserRepository.Setup(x => x.GetById(id)).Returns(user);

            var result = _userController.Get(id);

            result.DisplayName.Should().Be(userVM.DisplayName);

            result.Password.Should().Be(userVM.Password);

            result.Country.Should().Be(userVM.Country);

            result.Birthdate.Should().Be(userVM.Birthdate);

            result.EmailAdress.Should().Be(userVM.EmailAdress);
        }

    }
}
