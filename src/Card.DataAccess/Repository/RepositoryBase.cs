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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<List<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ValueTask<T> GetItemById(Guid Id)
        {
            return _dbSet.FindAsync(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> ListAsync()
        {
            return _dbSet.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<T> AddAsync(T entity)
        {
            _dbSet.AddAsync(entity);

            return Task.FromResult(entity);
        }
    }
}
