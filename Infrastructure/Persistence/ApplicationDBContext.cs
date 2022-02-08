using Application.Common.Interface;
using Domain.Common;
using Domain.Master;
using Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {

        #region Properties
        private readonly DateTime _currentDateTime;
        #endregion

        #region Ctor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
        {
            _currentDateTime = DateTime.Now;
        }
        #endregion

        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Category> Categories { get ; set ; }
        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Author = 1; //Get Current UsereID
                        entry.Entity.Created = _currentDateTime;
                        entry.Entity.Editor = 1; //Get Current UsereID
                        entry.Entity.Modified = _currentDateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Editor = 1; //Get Current UsereID
                        entry.Entity.Modified = _currentDateTime;
                        break;
                }
            }
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }
    }
}
