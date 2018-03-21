using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace musync.api.Models
{
    public class UserVM
    {
        public ObjectId Id { get; set; }

        public string DisplayName { get; set; }

        public string EmailAdress { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Country { get; set; }

        public string Password { get; set; }
    }
}
