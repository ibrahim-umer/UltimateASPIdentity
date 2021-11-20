using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UltimateASP.Data.DatabaseContext;
using UltimateASP.RepoOps.IRepo;

namespace UltimateASP.RepoOps.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly HotelListingDBContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(HotelListingDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task Delete(int Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> inclueds = null)
        {
            IQueryable<T> query = _dbSet;
            if(inclueds != null)
            {
                foreach(var includeProp in inclueds)
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> inclueds = null)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null) query.Where(expression);
            
            if (inclueds != null)
            {
                foreach (var includeProp in inclueds)
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null) query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
