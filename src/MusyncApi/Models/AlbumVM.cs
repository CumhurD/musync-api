using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace musync.api.Models
{
    public class AlbumVM
    {
        public ObjectId Id { get; set; }

        public string DisplayName { get; set; }

        public IEnumerable<ObjectId> SongIdentities { get; set; }

        public IEnumerable<ObjectId> ArtistsIdentities { get; set; }
    }
}
