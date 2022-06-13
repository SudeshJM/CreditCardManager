using AutoMapper;
using Card.Domain.Model;
using Card.Domain.Services;
using Card.Domain.Shared;
using CardAPI.Controllers;
using CardAPI.Dto;
using CardAPI.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardManagerTests.UnitTests
{
    [TestFixture]
    public class CardControllerUnitTest
    {
        private CardController _cardController;
        public CardControllerUnitTest()
        {
            // Initialise the controller object and mock objects.
            var logger = new Mock<ILogger<CardController>>();
            var cardService = new Mock<ICardService>();
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.IsSuccess = true;
            cardService.Setup(x => x.AddCreditCard(It.IsAny<CreditCard>())).Returns(Task.FromResult(serviceResult));
            cardService.Setup(x => x.GetCreditCards()).Returns(Task.FromResult(GetCardMockData()));
            Mapper mapper = new Mapper(new MapperConfiguration(config => config.AddProfile(new CardMappingProfile())));

            _cardController = new CardController(cardService.Object, mapper, logger.Object);
        }

        [Test]
        public async Task CardController_GetCards_ShouldReturnOk()
        {
            ActionResult<List<CardDto>> response = await _cardController.GetAllCards();
            OkObjectResult result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Value is List<CardDto>);
            List<CardDto> cards = result.Value as List<CardDto>;
            Assert.AreEqual(4, cards.Count);
        }

        [Test]
        public async Task CardController_AddCards_EmptyName()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12345";
            cardDto.Limit = 300;
            cardDto.Name = "";

            ActionResult<CardDto> response = await _cardController.AddCard(cardDto);
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task CardController_AddCards_NegativeLimit()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12345";
            cardDto.Name = "TestName";
            cardDto.Limit = -300;

            ActionResult<CardDto> response = await _cardController.AddCard(cardDto);
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task CardController_AddCards_EmptyCardNumber()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.Name = "TestName";
            cardDto.Limit = 300;
            cardDto.CardNumber = "";

            ActionResult<CardDto> response = await _cardController.AddCard(cardDto);
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task CardController_AddCards_NumberExceedingLength()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12312345678909789022334567878945";
            cardDto.Name = "TestName";
            cardDto.Limit = 5000;

            ActionResult<CardDto> response = await _cardController.AddCard(cardDto);
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task CardController_AddCards_InvalidLuhnCheckNumber()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12345";
            cardDto.Name = "TestName";
            cardDto.Limit = 5000;

            ActionResult<CardDto> response = await _cardController.AddCard(cardDto);
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task CardController_AddCards_InvalidCardNumber()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "123ad45";
            cardDto.Name = "TestName";
            cardDto.Limit = 5000;

            ActionResult<CardDto> response = await _cardController.AddCard(cardDto);
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task CardController_AddCards_ShouldReturnOk()
        {
            AddCardDto addCardDto = new AddCardDto();
            addCardDto.CardNumber = "347971545665469";
            addCardDto.Name = "TestName";
            addCardDto.Limit = 5000;

            ActionResult<CardDto> response = await _cardController.AddCard(addCardDto);
            OkObjectResult result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Value is CardDto);

            CardDto cardDto = result.Value as CardDto;
            Assert.AreEqual(addCardDto.CardNumber, cardDto.CardNumber);
        }

        private List<CreditCard> GetCardMockData()
        {
            List<CreditCard> cards = new List<CreditCard>
            {
                new CreditCard("TestName1", "3532823997700931", 3000),
                new CreditCard("TestName2", "5283210350299709", 200),
                new CreditCard("TestName3", "6225842860109074", 400),
                new CreditCard("TestName4", "501864693924960085", 40000)
            };

            return cards;
        }

    }
}
