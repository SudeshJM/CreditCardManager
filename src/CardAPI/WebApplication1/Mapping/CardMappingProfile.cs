using AutoMapper;
using Card.Domain.Model;
using CardAPI.Dto;

namespace CardAPI.Mapping
{
    /// <summary>
    /// Mapping profile for Credit Card dto 
    /// </summary>
    public class CardMappingProfile : Profile
    {
        /// <summary>
        /// Constructor to initialise mapping.
        /// </summary>
        public CardMappingProfile()
        {
            CreateMap<CreditCard, CardDto>().ReverseMap();
            CreateMap<CreditCard, AddCardDto>().ReverseMap();
        }
    }
}
