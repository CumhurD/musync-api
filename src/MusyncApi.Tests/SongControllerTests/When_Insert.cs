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
    public class When_Insert
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
        public void Should_Throw_BadRequest_If_Value_Is_Null()
        {
            SongVM value = null;

            var exception = Assert.Throws<HttpResponseException>(() => _songController.Insert(value));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Throw_BadRequest_If_There_Is_An_Exception()
        {
            SongVM song = new SongVM();

            _mockedSongRepository.Setup(x => x.Insert(It.IsAny<Song>())).Throws(new Exception());

            var exception = Assert.Throws<HttpResponseException>(() => _songController.Insert(song));

            exception.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Return_One_If_Inserted_Properly()
        {
            SongVM songVM = new SongVM()
            {
                DisplayName = "test",
                AlbumId = new ObjectId(),
                ArtistId = new ObjectId()
            };

            _mockedSongRepository.Setup(x => x.Insert(It.IsAny<Song>())).Returns(1);

            _songController.Insert(songVM).Should().Be(1);
        }

        [Test]
        public void The_Properties_Of_The_Song_Should_Be_The_Same()
        {
            SongVM songVM = new SongVM()
            {
                DisplayName = "test",
                AlbumId = new ObjectId(),
                ArtistId = new ObjectId()
            };

            Song insertedSong = new Song();

            _mockedSongRepository.Setup(x => x.Insert(It.IsAny<Song>())).Callback<Song>((insertSong) => insertedSong = insertSong);

            _songController.Insert(songVM);

            insertedSong.DisplayName.Should().Be(songVM.DisplayName);

            insertedSong.ArtistId.Should().Be(songVM.ArtistId);

            insertedSong.AlbumId.Should().Be(songVM.AlbumId);

        }

    }
}
