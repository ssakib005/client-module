using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.Models
{
    public class RefreshToken : Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string TokenString { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ExpiredOn { get; set; }

    }
}
