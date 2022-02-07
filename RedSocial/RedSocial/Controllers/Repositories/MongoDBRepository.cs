using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MongoDB.Driver;

using RedSocial.Controllers.Repositories;

namespace RedSocial.Controllers {
    public class MongoDBRepository<T> : IRepository<T> where T : class {
        public static MongoClient client;
        private IMongoCollection<T> collection;

        public MongoDBRepository(string tableName) {
            var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://jesus:Abril21@cluster0-shard-00-00.e8gml.mongodb.net:27017,cluster0-shard-00-01.e8gml.mongodb.net:27017,cluster0-shard-00-02.e8gml.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-mv6391-shard-0&authSource=admin&retryWrites=true&w=majority"));
            client = new MongoClient(settings);
            collection = client.GetDatabase("RedSocial").GetCollection<T>(tableName);
        }
        public async Task Create(T @object) => await collection.InsertOneAsync(@object);
        public async Task<T> Read(Expression<Func<T, bool>> func) => await collection.FindAsync(func).Result.FirstOrDefaultAsync();
        public async Task<IEnumerable<T>> Read() =>  await collection.FindAsync(x => true).Result.ToListAsync();
        public async Task Update(T @object, Expression<Func<T, bool>> func) => await collection.ReplaceOneAsync(func, @object);
        public async Task Delete(Expression<Func<T, bool>> func) => await collection.DeleteOneAsync(func);
    }
}