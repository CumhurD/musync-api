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

namespace musync.api.tests.AlbumControllerTests
{
    [TestFixture]
    public class When_Get_By_Id
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

            _mockedAlbumRepository.Setup(x => x.GetById(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _albumController.Get(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Return_AlbumVM_Properly()
        {
            ObjectId id = new ObjectId();

            AlbumVM albumVM = new AlbumVM
            {
                DisplayName = "test",
                ArtistsIdentities = new List<ObjectId>() { new ObjectId() },
                SongIdentities = new List<ObjectId>() { new ObjectId() }
            };

            Album album = new Album()
            {
                DisplayName = "test",
                ArtistsIdentities = new List<ObjectId>() { new ObjectId() },
                SongIdentities = new List<ObjectId>() { new ObjectId() }
            };

            _mockedAlbumRepository.Setup(x => x.GetById(id)).Returns(album);

            var result = _albumController.Get(id);

            result.DisplayName.Should().Be(albumVM.DisplayName);

            result.Id.Should().Be(albumVM.Id);

            result.ArtistsIdentities.ToList()[0].Should().Be(albumVM.ArtistsIdentities.ToList()[0]);

            result.SongIdentities.ToList()[0].Should().Be(albumVM.SongIdentities.ToList()[0]);
        }
    }
}
