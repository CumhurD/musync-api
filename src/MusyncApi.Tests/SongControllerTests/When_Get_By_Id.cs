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
    public class When_Get_By_Id
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

            _mockedSongRepository.Setup(x => x.GetById(It.IsAny<ObjectId>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _songController.Get(objectId));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Return_SongVM_Properly()
        {
            ObjectId id = new ObjectId();

            SongVM songVM = new SongVM()
            {
                Id = new ObjectId(),
                DisplayName = "test1",
                AlbumId = new ObjectId(),
                ArtistId = new ObjectId()
            };

            Song song = new Song()
            {
                Id = new ObjectId(),
                DisplayName = "test1",
                AlbumId = new ObjectId(),
                ArtistId = new ObjectId()
            };

            _mockedSongRepository.Setup(x => x.GetById(id)).Returns(song);

            var result = _songController.Get(id);

            result.DisplayName.Should().Be(songVM.DisplayName);

            result.Id.Should().Be(songVM.Id);

            result.ArtistId.Should().Be(songVM.ArtistId);

            result.AlbumId.Should().Be(songVM.AlbumId);
        }

    }
}
