using Card.Domain.Model;
using System;

namespace Card.Domain.Model
{
    public class CreditCard : Entity
    {

        public CreditCard()
        {
            this.Id = Guid.NewGuid();
            this.Balance = 0;
            this.Currency = "AED"; //this should be taken from list of available currencies.
        }
        public string Name { get; set; }

        public string CardNumber { get; set; }

        public Decimal  Balance { get; set; } 

        public Decimal Limit { get; set; }

        public string Currency { get; set; }
    }
}
