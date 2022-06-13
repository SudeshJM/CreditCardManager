using Card.Domain.Model;
using Card.Domain.Repository;
using Card.Domain.Services;
using Card.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card.Services
{
    public class CardService : ICardService
    {
        private ICardRepository _cardRepository;
        private IUnitOfWork _unitOfWork;
        public CardService(ICardRepository cardRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditCard"></param>
        /// <returns></returns>
        public async Task<ServiceResult> AddCreditCard(CreditCard creditCard)
        {
            ServiceResult result = new ServiceResult();
            List<CreditCard> cards = await _cardRepository.GetAsync(x => x.CardNumber == creditCard.CardNumber || x.Name == creditCard.Name);
            if (cards.Count > 0)
            {
                result.Error = "A card with the same card number or name exists. Try adding card with a different name or card number.";
                return result;
            }

           await _cardRepository.AddAsync(creditCard);
           await  _unitOfWork.SaveChangesAsync();
            result.IsSuccess = true;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<CreditCard>> GetCreditCards()
        {
            return await _cardRepository.ListAsync();
        }
    }
}
