using Card.DataAccess.Configuration;
using Card.Domain.Model;
using Card.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Card.DataAccess.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : Entity
    {

        private readonly DbSet<T> _dbSet;

        protected RepositoryBase(CardDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();

        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.SingleOrDefaultAsync(expression);
        }

        public ValueTask<T> GetItemById(Guid Id)
        {
            return _dbSet.FindAsync(Id);
        }

        public Task<List<T>> ListAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<T> AddAsync(T entity)
        {
            _dbSet.AddAsync(entity);

            return Task.FromResult(entity);
        }
    }
}
