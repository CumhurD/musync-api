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

namespace musync.api.tests.SongControllerTests
{
    [TestFixture]
    public class When_Super_Delete
    {
        private Mock<ISongRepository> _mockedSongRepository;

        private SongController _songController;

        [SetUp]
        public void Setup()
        {
            _mockedSongRepository = new Mock<ISongRepository>();

            _songController = new SongController(_mockedSongRepository.Object);
        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_An_Exception()
        {
            ObjectId objectId = new ObjectId();

            _mockedSongRepository.Setup(x => x.SuperDelete(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _songController.SuperDelete(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Test]
        public void Should_Return_One_If_Deleted_Properly()
        {
            ObjectId id = new ObjectId();

            _mockedSongRepository.Setup(x => x.SuperDelete(id)).Returns(1);

            _songController.SuperDelete(id).Should().Be(1);
        }
    }
}

