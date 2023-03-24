using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Bussiness.Repository.Interfaces;

namespace HapusPlant.Bussiness.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkHapus
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        void CreateTransaction();
        Task CreateTransactionAsync();
        void Commit();
        Task CommitAsync();
        void Dispose();
        void Rollback();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}