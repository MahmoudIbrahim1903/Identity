using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);

        List<T> GetAll(Expression<Func<T, bool>> criteria = null);

        bool Any(Expression<Func<T, bool>> criteria);

        bool Add(T entity);
    }
}
