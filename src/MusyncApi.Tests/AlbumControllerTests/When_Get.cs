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

namespace musync.api.tests.AlbumControllerTests
{
    [TestFixture]
    public class When_Get
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
        public void Should_Return_List_AlbumVM_Properly()
        {

            List<AlbumVM> albumVMList = new List<AlbumVM>()
            {
                new AlbumVM()
                {
                    Id= new ObjectId(),
                    DisplayName= "test1",
                    ArtistsIdentities = new List<ObjectId>(){new ObjectId()},
                    SongIdentities = new List<ObjectId>() { new ObjectId()}
                }
            };



            var albums = new List<Album>()
            {
                new Album()
                {
                    Id= new ObjectId(),
                    DisplayName= "test1",
                    ArtistsIdentities = new List<ObjectId>(){new ObjectId()},
                    SongIdentities = new List<ObjectId>() { new ObjectId()}
                }
            }.AsQueryable();

            _mockedAlbumRepository.Setup(x => x.GetAll()).Returns(albums);

            var result = _albumController.Get();

            result[0].DisplayName.Should().Be(albumVMList[0].DisplayName);

            result[0].Id.Should().Be(albumVMList[0].Id);

            result[0].ArtistsIdentities.ToList()[0].Should().Be(albumVMList[0].ArtistsIdentities.ToList()[0]);

            result[0].SongIdentities.ToList()[0].Should().Be(albumVMList[0].SongIdentities.ToList()[0]);
        }
    }
}
