using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using RedSocial.Models;

namespace RedSocial.Controllers {
    public interface IRepository<T> {
        Task<IEnumerable<T>> Read();
        Task<T> Read(Expression<Func<User, bool>> filter);
        Task Create(T user);
        Task Update(T user);
        Task Delete(string id);
    }
}
