using System;
using System.Collections.Generic;
using System.Text;
using musync.api.Controllers;
using musync.api.Repository.Interfaces;
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
    }
}
