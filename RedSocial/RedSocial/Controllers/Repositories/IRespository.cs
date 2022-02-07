using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Controllers.Repositories {
    public interface IRepository<T> {
        Task<IEnumerable<T>> Read();
        Task<T> Read(Expression<Func<T, bool>> filter);
        Task Create(T user);
        Task Update(T user, Expression<Func<T, bool>> func);
        Task Delete(Expression<Func<T, bool>> func);
    }
}
