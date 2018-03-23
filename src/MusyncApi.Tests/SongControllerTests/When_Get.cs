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

namespace musync.api.tests.SongControllerTests
{
    [TestFixture]
    public class When_Get
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
        public void Should_Return_List_SongVM_Properly()
        {

            List<SongVM> songVMList = new List<SongVM>()
            {
                new SongVM()
                {
                    Id= new ObjectId(),
                    DisplayName= "test1",
                    AlbumId = new ObjectId(),
                    ArtistId = new ObjectId()
                }
            };

            var songs = new List<Song>()
            {
                new Song()
                {
                    Id= new ObjectId(),
                    DisplayName= "test1",
                    AlbumId = new ObjectId(),
                    ArtistId = new ObjectId()
                }
            }.AsQueryable();

            _mockedSongRepository.Setup(x => x.GetAll()).Returns(songs);

            var result = _songController.Get();

            result[0].DisplayName.Should().Be(songVMList[0].DisplayName);

            result[0].Id.Should().Be(songVMList[0].Id);

            result[0].ArtistId.Should().Be(songVMList[0].ArtistId);

            result[0].AlbumId.Should().Be(songVMList[0].AlbumId);
        }
    }
}
