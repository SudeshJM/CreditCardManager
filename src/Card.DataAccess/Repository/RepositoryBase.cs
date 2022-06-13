using Card.DataAccess.Configuration;
using Card.Domain.Model;
using Card.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        /// Gets items based on specified filter.
        /// </summary>
        /// <param name="expression">The filter expression</param>
        /// <returns>The list of items</returns>
        public Task<List<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToListAsync();
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>The list of items</returns>
        public Task<List<T>> ListAsync()
        {
            return _dbSet.ToListAsync();
        }

        /// <summary>
        /// Adds item to database.
        /// </summary>
        /// <param name="entity">Adds entity to database</param>
        /// <returns>The added object.</returns>
        public Task<T> AddAsync(T entity)
        {
            _dbSet.AddAsync(entity);

            return Task.FromResult(entity);
        }
    }
}
