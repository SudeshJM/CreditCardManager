using Card.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Repository
{
    public interface ICardRepository: IRepository<CreditCard>
    {
    }
}
