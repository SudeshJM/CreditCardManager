using Card.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Card.Domain.Repository
{
    public interface IRepository<T> where T : Entity
    {
        public Task<List<T>> GetAsync(Expression<Func<T, bool>> expression);

        public ValueTask<T> GetItemById(Guid Id);

        public Task<List<T>> ListAsync();

        public Task<T> AddAsync(T entity);
    }
}