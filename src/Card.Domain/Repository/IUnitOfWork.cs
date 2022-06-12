using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
