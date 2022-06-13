using Card.DataAccess.Configuration;
using Card.Domain.Model;
using Card.Domain.Repository;

namespace Card.DataAccess.Repository
{
    public class CardRepository : RepositoryBase<CreditCard>, ICardRepository
    {
        public CardRepository(CardDbContext dbContext) : base(dbContext)
        {

        }
    }
}
