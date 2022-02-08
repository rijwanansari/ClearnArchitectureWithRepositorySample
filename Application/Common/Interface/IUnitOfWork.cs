using Domain.Master;
using Domain.Product;

namespace Application.Common.Interface
{
    public interface IUnitOfWork
    {
        IRepository<AppSetting> AppSettingRepo { get; }
        IRepository<Category> CategoryRepo { get; }
        Task<int> SaveAsync();
        int Save();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
