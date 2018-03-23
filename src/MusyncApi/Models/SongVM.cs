using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace musync.api.Models
{
    public class SongVM
    {
        public ObjectId Id { get; set; }

        public string DisplayName { get; set; }

        public ObjectId AlbumId { get; set; }

        public ObjectId ArtistId { get; set; }
    }
}
