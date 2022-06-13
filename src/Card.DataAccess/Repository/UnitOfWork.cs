using Card.DataAccess.Configuration;
using Card.Domain.Repository;
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

        /// <summary>
        /// Saves changes to database asynchronously.
        /// </summary>
        /// <returns>Count of updated/added/removed rows.</returns>
        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Saves changes synchronously.
        /// </summary>
        /// <returns>ount of updated/added/removed rows.</returns>
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
