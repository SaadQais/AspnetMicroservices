using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly OrderDbContext _context;

        public RepositoryBase(OrderDbContext context)
        {
            _context = context;
        }
        
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null) query = query.Where(predicate);

            if (!string.IsNullOrEmpty(includeString)) query = query.Include(includeString);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            if (disableTracking) query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null) query = query.Where(predicate);

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (orderBy != null) return await orderBy(query).ToListAsync();

            if (disableTracking) query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }        

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
