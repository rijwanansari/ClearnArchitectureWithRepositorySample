using Application.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    internal class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Properties
        private readonly ApplicationDBContext _context;

        protected DbSet<TEntity> _entities;
        #endregion
        #region Ctor

        public EfRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion
        #region Utility
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }
        #endregion

        #region Repository Methods
        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }
        public virtual void Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }

        }        
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            try
            {
                Entities.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
            //catch (DbUpdateException exception)
            //{
            //    //ensure that the detailed error text is saved in the Log
            //    throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            //}
        }
        public virtual async Task<bool> Delete(object id)
        {
            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity == null)
                    return false;

                Entities.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task<bool> Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return false;

                Entities.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task<bool> Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entities = Entities.Where(where);
                Entities.RemoveRange(entities);
                return true;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual async Task<TEntity> Get(object id)
        {
            try
            {
                return await Entities.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await Entities.FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            try
            {
                return Entities;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return Entities.Where(where);
        }
        public virtual void Save()
        {
            _context.SaveChanges();
        }
        public virtual void SaveAsync()
        {
            _context.SaveChangesAsync();
        }
        public virtual async Task<int> Count()
        {
            return await Entities.CountAsync();

        }
        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> where)
        {
            return await Entities.CountAsync(where);

        }
        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        #endregion
    }
}
