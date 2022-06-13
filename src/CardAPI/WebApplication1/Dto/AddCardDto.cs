using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardAPI.Dto
{
    /// <summary>
    /// DTO to handle add card request.
    /// </summary>
    public class AddCardDto
    {
        [Required]
        [MaxLength(100)]
        [DataType("String")]
        public string Name { get; set; }

        [Required]
        [DataType("String")]
        [MaxLength(19)]
        public string CardNumber { get; set; }

        [Required]
        [DataType("Decimal")]
        [Range(0, 100000000)]
        public Decimal Limit { get; set; }
    }
}
