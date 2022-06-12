using Card.Domain.Model;
using Card.Domain.Repository;
using Card.Domain.Services;
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

        public async Task AddCreditCard(CreditCard creditCard)
        {
           await _cardRepository.AddAsync(creditCard);
           await  _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CreditCard>> GetCreditCards()
        {
            return await _cardRepository.ListAsync();
        }
    }
}
