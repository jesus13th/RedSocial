using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using RedSocial.Models;

using SQLite;

namespace RedSocial.Controllers {
    /*public class SQLiteRepository : IRepository<User> {
        public SQLiteAsyncConnection Connection { get; set; }

        public SQLiteRepository(string path = "") {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RedSocial.db3");

            Connection = new SQLiteAsyncConnection(path);
            Connection.CreateTableAsync<User>().Wait();
        }

        public async void Delete(string id) => await Connection.DeleteAsync(Connection.Table<User>().FirstOrDefaultAsync(x => x.Id == id));

        public async Task<User> Get(string id) => await Connection.Table<User>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<User>> GetAll() => await Connection.Table<User>().ToListAsync();

        public async Task Insert(User user) => await Connection.InsertAsync(user);

        public async void Update(User user) => await Connection.UpdateAsync(user);
    }*/
}
