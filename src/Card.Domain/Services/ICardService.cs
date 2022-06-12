using Card.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Services
{
    public interface ICardService
    {
        public Task<List<CreditCard>> GetCreditCards();

        public Task AddCreditCard(CreditCard creditCard);
    }
}
