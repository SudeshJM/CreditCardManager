using Card.DataAccess.Configuration;
using Card.Domain.Model;
using Card.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Card.DataAccess.Repository
{
    public class CardRepository : RepositoryBase<CreditCard>, ICardRepository
    {
        public CardRepository(CardDbContext dbContext) : base(dbContext)
        {

        }
    }
}
