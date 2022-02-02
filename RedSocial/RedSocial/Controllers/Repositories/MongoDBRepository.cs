using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MongoDB.Driver;

using RedSocial.Models;

namespace RedSocial.Controllers {
    internal class MongoDBRepository : IRepository<User> {
        public static MongoClient client;
        private IMongoCollection<User> collection;

        public MongoDBRepository() {
            var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://jesus:Abril21@cluster0-shard-00-00.e8gml.mongodb.net:27017,cluster0-shard-00-01.e8gml.mongodb.net:27017,cluster0-shard-00-02.e8gml.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-mv6391-shard-0&authSource=admin&retryWrites=true&w=majority"));
            client = new MongoClient(settings);
            collection = client.GetDatabase("RedSocial").GetCollection<User>("Users");
        }
        public async Task Create(User user) => await collection.InsertOneAsync(user);
        public async Task<User> Read(Expression<Func<User, bool>> func) => await collection.FindAsync(func).Result.FirstOrDefaultAsync();
        public async Task<IEnumerable<User>> Read() =>  await collection.FindAsync(x => true).Result.ToListAsync();
        public async Task Update(User user) => await collection.ReplaceOneAsync(x => x.Id == user.Id, user);
        public async Task Delete(string id) => await collection.DeleteOneAsync(p => p.Id == id);
    }
}