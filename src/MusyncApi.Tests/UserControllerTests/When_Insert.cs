using System;
using System.Collections.Generic;
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
    public class When_Insert
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
        public void Should_Throw_BadRequest_If_Value_Is_Null()
        {
            UserVM value = null;

            var exception = Assert.Throws<HttpResponseException>(() => _userController.Insert(value));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_An_Exception()
        {
            UserVM user = new UserVM();

            _mockedUserRepository.Setup(x => x.Insert(It.IsAny<User>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _userController.Insert(user));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Return_One_If_Inserted_Properly()
        {
            UserVM userVM = new UserVM()
            {
                DisplayName = "test",
                EmailAdress = "testMail",
                Country = "testCountry",
                Password = "123",
                Birthdate = DateTime.Today
            };

            _mockedUserRepository.Setup(x => x.Insert(It.IsAny<User>())).Returns(1);

            _userController.Insert(userVM).Should().Be(1);
        }

        [Test]
        public void The_Properties_Of_The_User_Should_Be_The_Same()
        {
            UserVM userVM = new UserVM()
            {
                DisplayName = "test",
                EmailAdress = "testMail",
                Country = "testCountry",
                Password = "123",
                Birthdate = DateTime.Today
            };

            User insertedUser = new User();

            _mockedUserRepository.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>((insertUser) => insertedUser = insertUser);

            _userController.Insert(userVM);

            insertedUser.DisplayName.Should().Be(userVM.DisplayName);

            insertedUser.EmailAdress.Should().Be(userVM.EmailAdress);

            insertedUser.Country.Should().Be(userVM.Country);

            insertedUser.Password.Should().Be(userVM.Password);

        }
    }
}
