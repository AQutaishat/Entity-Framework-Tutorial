using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EntityFramework_Demo
{
    public class Repository<TEntity, TContext> : ReadOnlyRepository<TEntity, TContext>
        where TEntity : class, new()
        where TContext : DbContext, new()

    {
        public Repository()
            : base()
        {
        }

        public Repository(TContext context)
            : base(context)
        {
        }        

        public virtual TEntity Create(TEntity entity, bool Save = true) //, string createdBy = null)
        {

            //entity.CreatedDate = DateTime.UtcNow;
            //entity.CreatedBy = createdBy;
            TEntity Result = this.Entities.Add(entity);
            if (Save)
            {
                this.Save();
            }
            return Result;
        }

        public virtual TEntity Update(TEntity entity, bool Save = true) //, string modifiedBy = null)
        {
            TEntity Result = entity;
            //entity.ModifiedDate = DateTime.UtcNow;
            //entity.ModifiedBy = modifiedBy;
            this.Entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            if (Save)
            {
                this.Save();
            }
            return Result;
        }

        public virtual TEntity Delete(object id, bool Save = true)
        {
            TEntity entity = this.Entities.Find(id);
            var Result = Delete(entity, Save);
            return Result;
        }

        public virtual TEntity Delete(TEntity entity, bool Save = true)
        {
            var dbSet = this.Entities;
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            var Result = dbSet.Remove(entity);
            if (Save)
            {
                this.Save();
            }
            return Result;

        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }
    }

}