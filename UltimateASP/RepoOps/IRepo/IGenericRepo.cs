using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UltimateASP.RepoOps.IRepo
{
    public interface IGenericRepo<T> where T: class
    {
        Task<IList<T>> GetAll(
                Expression<Func<T, bool>> expression = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                List<string> inclueds =null
            );
        Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> inclueds = null);
        Task Insert(T entity);
        void Update(T entity);
        Task Delete(int Id);
        Task InsertRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);



    }
}
