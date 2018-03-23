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
    public class When_Insert
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
        public void Should_Throw_BadRequest_If_Value_Is_Null()
        {
            AlbumVM value = null;

            var exception = Assert.Throws<HttpResponseException>(() => _albumController.Insert(value));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_An_Exception()
        {
            AlbumVM album = new AlbumVM();

            _mockedAlbumRepository.Setup(x => x.Insert(It.IsAny<Album>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _albumController.Insert(album));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Return_One_If_Inserted_Properly()
        {
            AlbumVM albumVM = new AlbumVM
            {
                DisplayName = "test",
                ArtistsIdentities = new List<ObjectId>() { new ObjectId() },
                SongIdentities = new List<ObjectId>() { new ObjectId() }
            };

            _mockedAlbumRepository.Setup(x => x.Insert(It.IsAny<Album>())).Returns(1);

            _albumController.Insert(albumVM).Should().Be(1);
        }

        [Test]
        public void The_Properties_Of_The_ALbum_Should_Be_The_Same()
        {
            AlbumVM albumVM = new AlbumVM()
            {
                DisplayName = "test",
                ArtistsIdentities = new List<ObjectId>() { new ObjectId() },
                SongIdentities = new List<ObjectId>() { new ObjectId() }
            };

            Album insertedAlbum = new Album();

            _mockedAlbumRepository.Setup(x => x.Insert(It.IsAny<Album>())).Callback<Album>((insertAlbum) => insertedAlbum = insertAlbum);

            _albumController.Insert(albumVM);

            insertedAlbum.DisplayName.Should().Be(albumVM.DisplayName);

            insertedAlbum.ArtistsIdentities.ToList()[0].Should().Be(albumVM.ArtistsIdentities.ToList()[0]);

            insertedAlbum.SongIdentities.ToList()[0].Should().Be(albumVM.SongIdentities.ToList()[0]);
        }
    }
}
