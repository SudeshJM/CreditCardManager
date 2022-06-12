using AutoMapper;
using Card.Domain.Model;
using CardAPI.Dto;

namespace CardAPI.Mapping
{
    public class CardMappingProfile : Profile
    {
        public CardMappingProfile()
        {
            CreateMap<CreditCard, CardDto>().ReverseMap();
            CreateMap<CreditCard, AddCardDto>().ReverseMap();
        }
    }
}
