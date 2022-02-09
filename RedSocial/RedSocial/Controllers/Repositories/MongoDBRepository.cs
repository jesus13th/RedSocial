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
        private readonly string connectionString = "";

        public MongoDBRepository(string tableName) {
            connectionString = App.Credentials.MongoDB.ConnectionString;
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
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