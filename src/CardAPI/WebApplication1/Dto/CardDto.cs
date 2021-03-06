using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardAPI.Dto
{
    /// <summary>
    /// DTO to handle card info.
    /// </summary>
    public class CardDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CardNumber { get; set; }

        public Decimal Balance { get; set; }

        public Decimal Limit { get; set; }

        public string Currency { get; set; }
    }
}
