using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace RedSocial.Models {
    public class Post {
        [BsonId, BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        [BsonElement("Owner")] public string OwnerId { get; set; }
        [BsonElement("Description")] public string Description { get; set; }
        [BsonElement("ImgName")] public string ImageName { get; set; }
        [BsonElement("Date")] public DateTime Date { get; private set; }

        public Post(string OwnerId, string Description, string ImageName) {
            this.Description = Description;
            this.ImageName = ImageName;
            this.OwnerId = OwnerId;
            Date = DateTime.Now;
        }
        public async Task Publish() {
            await App.RepositoryPosts.Create(this);
        }
        public string ImageUrl => $"https://s3.us-east-2.amazonaws.com/spookydevstudio.redsocial/{this.ImageName}"; 
    }
}
