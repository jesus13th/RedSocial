using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQLite;

namespace RedSocial.Models {
    public class User {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("DateOfBorn")]
        public string DateOfBorn { get; set; }
        [BsonElement("ProfileImg")]
        public string ProfileImgName { get; set; }
        public string GetProfileImg => @$"https://s3.us-east-2.amazonaws.com/spookydevstudio.redsocial/{ProfileImgName}";
    }
}
