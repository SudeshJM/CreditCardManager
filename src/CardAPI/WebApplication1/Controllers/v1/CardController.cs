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
    /// <summary>
    /// This Controller handle Card api requests.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CardController : Controller
    {

        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor to initialise card controller
        /// </summary>
        /// <param name="cardService">The card service.</param>
        /// <param name="mapper">The card mapping profile.</param>
        /// <param name="logger">The logger.</param>
        public CardController(ICardService cardService, IMapper mapper, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Api to get Credit Card list.
        /// </summary>
        /// <returns>The list of credit cards or failure.</returns>
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
        /// Api to add credit card to database.
        /// </summary>
        /// <param name="newCard">The new card object.</param>
        /// <returns>The added card or failure response.</returns>
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
                    return BadRequest(validationResult);

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
