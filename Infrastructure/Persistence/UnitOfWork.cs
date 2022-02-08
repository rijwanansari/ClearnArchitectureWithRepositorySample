using Application.Common.Interface;
using Domain.Master;
using Domain.Product;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly ApplicationDBContext _context;
        IDbContextTransaction dbContextTransaction;
        private IRepository<AppSetting> _appSettingRepo;
        private IRepository<Category> _categoryRepo;
        #endregion
        #region Ctor
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Repository/*
        public IRepository<AppSetting> AppSettingRepo
        {
            get
            {
                if (this._appSettingRepo == null)
                    this._appSettingRepo = new EfRepository<AppSetting>(_context);
                return _appSettingRepo;
            }
        }
        public IRepository<Category> CategoryRepo
        {
            get
            {
                if (this._categoryRepo == null)
                    this._categoryRepo = new EfRepository<Category>(_context);
                return _categoryRepo;
            }
        }
        #endregion
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void BeginTransaction()
        {
            dbContextTransaction = _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Commit();
            }
        }
        public void RollbackTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Rollback();
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
