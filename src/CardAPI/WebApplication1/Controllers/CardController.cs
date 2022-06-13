using CardAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Card.Domain.Services;
using Card.Domain.Model;
using AutoMapper;
using Microsoft.AspNetCore.Routing;
using CardAPI.Validators;
using System.Net;
using Microsoft.Extensions.Logging;
using Card.Domain.Shared;

namespace CardAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CardController : Controller
    {

        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CardController(ICardService cardService, IMapper mapper, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Cards")]
        public async Task<ActionResult<List<CardDto>>> GetAllCards()
        {
            try
            {
                List<CreditCard> cards = await _cardService.GetCreditCards();
                List<CardDto> cardResponse = _mapper.Map<List<CreditCard>, List<CardDto>>(cards);
                return Ok(cardResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem("Unable to get credit cards.", string.Empty, (int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCard"></param>
        /// <returns></returns>
        [HttpPost("AddCard")]
        public async Task<ActionResult<CardDto>> AddCard([FromBody]AddCardDto newCard)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                CreditCard creditCard = _mapper.Map<AddCardDto, CreditCard>(newCard);
                ValidationResult validationResult = creditCard.ValidateCreditCard();
                if (!validationResult.IsSuccess)
                    return ValidationProblem(validationResult.Error);

                ServiceResult result= await _cardService.AddCreditCard(creditCard);
                if(!result.IsSuccess)
                    return Problem(result.Error, string.Empty, (int)HttpStatusCode.InternalServerError);

                CardDto cardResponse = _mapper.Map<CreditCard, CardDto>(creditCard);
                return Ok(cardResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem("Unable to add the credit card.", string.Empty, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
