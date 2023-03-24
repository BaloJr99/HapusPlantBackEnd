using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Bussiness.Repository.Interfaces;
using HapusPlant.Bussiness.Repository.Services;
using HapusPlant.Bussiness.UnitOfWork.Interfaces;
using HapusPlant.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HapusPlant.Bussiness.UnitOfWork.Services
{
    public class UnitOfWorkHapus: IUnitOfWorkHapus, IDisposable
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction _objTransaction;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWorkHapus(HapusplantContext dbContext)
        {
            _dbContext = dbContext;
            //Ensures that the db was created
            _objTransaction = null!;
            _dbContext.Database.EnsureCreated();
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if(_repositories.Keys.Contains(typeof(TEntity))){
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public void Commit()
        {
            _objTransaction.Commit();
        }

        public async Task CommitAsync()
        {
            await _objTransaction.CommitAsync();
        }

        public void CreateTransaction()
        {
            _objTransaction = _dbContext.Database.BeginTransaction();
        }

        public async Task CreateTransactionAsync()
        {
            _objTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            if(_dbContext != null){
                _dbContext.Dispose();
            }
        }

        public void Rollback()
        {
            _objTransaction.Rollback();
            _objTransaction.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _objTransaction.RollbackAsync();
            await _objTransaction.DisposeAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}