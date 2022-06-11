using System;

namespace Card.Domain
{
    public class CreditCard
    {
        public string Name { get; set; }

        public string CardNumber { get; set; }

        public Decimal  Balance { get; set; } 

        public Decimal Limit { get; set; }
    }
}
