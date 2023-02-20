using IdentityWithExistingDb.Core.Repositories;
using IdentityWithExistingDb.Ef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.EF.Managers
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T GetById(int id) => _context.Set<T>().Find(id);

        public List<T> GetAll(Expression<Func<T, bool>> criteria = null)
        {
            if(criteria == null)
                return _context.Set<T>().ToList();
            else
                return _context.Set<T>().Where(criteria).ToList();
        }

        public bool Any(Expression<Func<T, bool>> criteria) => _context.Set<T>().Any(criteria);

        public bool Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
