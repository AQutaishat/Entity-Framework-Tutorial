using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using System.Linq.Dynamic;
namespace EntityFramework_Demo
{
    public class ReadOnlyRepository<TEntity, TContext> 
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        protected readonly TContext context;
        public ReadOnlyRepository()
        {
            this.context = new TContext();
        }

        public ReadOnlyRepository(TContext context)
        {
            this.context = context;
        }

        public DbSet<TEntity> Entities
        {
            get
            {
                var Result = this.context.Set<TEntity>();
                return Result;
            }
        }
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return GetQueryable(null, null, null, null, null);
        }
        public virtual IQueryable<TEntity> GetQueryable(bool asNoTracking)
        {
            return GetQueryable(null, null, null, null, null, asNoTracking);
        }
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return GetQueryable(filter, null, null, null, null);
        }
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter, bool asNoTracking)
        {
            return GetQueryable(filter, null, null, null, null, asNoTracking);
        }
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            return GetQueryable(filter, null, includeProperties, null, null);
        }
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter, string includeProperties, bool asNoTracking)
        {
            return GetQueryable(filter, null, includeProperties, null, null, asNoTracking);
        }
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter, string includeProperties, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, null, null);
        }


        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter, string includeProperties, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool asNoTracking)
        {
            return GetQueryable(filter, orderBy, includeProperties, null, null, asNoTracking);
        }

        public virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = false)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = this.Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        public virtual IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = false)
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take, asNoTracking);
        }

        public virtual async Task<IEnumerable> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = false)
        {
            return await GetQueryable(null, orderBy, includeProperties, skip, take, asNoTracking).ToListAsync();
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = false)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take, asNoTracking);
        }

        public virtual async Task<IEnumerable> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = false)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take, asNoTracking).ToListAsync();
        }

        public virtual TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "",
            bool asNoTracking = false)
        {
            return GetQueryable(filter, null, includeProperties, null, null, asNoTracking).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool asNoTracking = false)
        {
            return await GetQueryable(filter, null, includeProperties, null, null, asNoTracking).SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "",
            bool asNoTracking = false)
        {
            return GetQueryable(filter, orderBy, includeProperties, null, null, asNoTracking).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool asNoTracking = false)
        {
            return await GetQueryable(filter, orderBy, includeProperties, null, null, asNoTracking).FirstOrDefaultAsync();
        }



        public virtual TEntity GetById(object id)
        {
            var query = this.Entities;
            var result = query.Find(id);
            return result;
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return this.Entities.FindAsync(id);
        }




        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }




    }

}