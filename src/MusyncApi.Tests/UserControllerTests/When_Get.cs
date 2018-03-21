using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class When_Get
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
        public void Should_Return_List_UserVM_Properly()
        {

            List<UserVM> userVMList = new List<UserVM>()
            {
                new UserVM()
                {
                    Id= new ObjectId(),
                    DisplayName= "test1",
                    Country = "testCountry",
                    Birthdate = DateTime.Today,
                    Password = "123",
                    EmailAdress = "testEmail1"
                }
            };

            var users = new List<User>()
            {
                new User()
                {
                    Id= new ObjectId(),
                    DisplayName= "test1",
                    Country = "testCountry",
                    Birthdate = DateTime.Today,
                    Password = "123",
                    EmailAdress = "testEmail1"
                }
            }.AsQueryable();

            _mockedUserRepository.Setup(x => x.GetAll()).Returns(users);

            var result = _userController.Get();

            result[0].DisplayName.Should().Be(userVMList[0].DisplayName);

            result[0].Password.Should().Be(userVMList[0].Password);

            result[0].Country.Should().Be(userVMList[0].Country);

            result[0].Birthdate.Should().Be(userVMList[0].Birthdate);

            result[0].EmailAdress.Should().Be(userVMList[0].EmailAdress);
        }
    }
}
