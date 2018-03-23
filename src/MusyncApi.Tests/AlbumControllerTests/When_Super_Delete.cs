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

namespace musync.api.tests.AlbumControllerTests
{
    [TestFixture]
    public class When_Super_Delete
    {
        private Mock<IAlbumRepository> _mockedAlbumRepository;

        private AlbumController _albumController;

        [SetUp]
        public void Setup()
        {
            _mockedAlbumRepository = new Mock<IAlbumRepository>();

            _albumController = new AlbumController(_mockedAlbumRepository.Object);
        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_An_Exception()
        {
            ObjectId objectId = new ObjectId();

            _mockedAlbumRepository.Setup(x => x.SuperDelete(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _albumController.SuperDelete(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Test]
        public void Should_Return_One_If_Deleted_Properly()
        {
            ObjectId id = new ObjectId();

            _mockedAlbumRepository.Setup(x => x.SuperDelete(id)).Returns(1);

            _albumController.SuperDelete(id).Should().Be(1);
        }
    }
}
