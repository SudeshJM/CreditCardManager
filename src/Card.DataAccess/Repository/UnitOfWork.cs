using Card.DataAccess.Configuration;
using Card.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Card.DataAccess.Repository 
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CardDbContext _dbContext;

        public UnitOfWork(CardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
