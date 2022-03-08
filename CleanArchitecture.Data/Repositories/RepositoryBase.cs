using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {

        protected readonly StreamerDBContext _context;


        public RepositoryBase(StreamerDBContext context)
        {
            _context = context; 

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
         
            return  await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
           return await _context.Set<T>().Where(predicate).ToListAsync();

        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string IncludeString = null, bool disableTracking = true)
        {
           //Instanciamos IQueryable
           IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if(!string.IsNullOrWhiteSpace(IncludeString))
                query = query.Include(IncludeString);
            if(predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
          
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, string IncludeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if(includes != null)
                query = includes.Aggregate(query,(current,include) => current.Include(include));

            if (!string.IsNullOrWhiteSpace(IncludeString))
                query = query.Include(IncludeString);
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();


            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await _context.Set<T>().FindAsync(id);
        }


        public async Task<T> AddAsync(T entity)
        {
             _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public void AddEntity(T entity)
        {
            //no se hace async porque esa tarea la hace unitofwork
           
           _context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
            
        }
    }
}
